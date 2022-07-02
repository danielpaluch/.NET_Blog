using Microsoft.AspNetCore.Mvc;
using BlogAPI.Entities;
using AutoMapper;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using BlogAPI.Services;
using Microsoft.AspNetCore.Identity;
using BlogAPI.Exceptions;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BlogAPI.Services
{
    public interface IUserService
    {
        User CreateUser(RegisterUserDto dto);
        int DeleteUser(int id);
        IEnumerable<User> GetUsers();
        string GenerateToken(LoginDto dto);
    }

    public class UserService : IUserService
    {
        private readonly BlogDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authenticationSettings;

        public UserService(BlogDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.authenticationSettings = authenticationSettings;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = dbContext
                .Users
                .Include(e => e.Role)
                .ToList();

            return users;
        }
        public User CreateUser(RegisterUserDto dto)
        {

            var user = new User()
            {
                Name = dto.Name,
                Email = dto.Email,
                RoleId = dto.RoleId
            };

            var hashedPassword = passwordHasher.HashPassword(user, dto.Password);
            user.Password = hashedPassword;


            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return user;
        }
        public int DeleteUser(int id)
        {
            var user = dbContext.Users.Where(e => e.Id == id).FirstOrDefault();

            if (user == null)
                return -1;

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

            return id;
        }
        public string GenerateToken(LoginDto dto)
        {
            var user = dbContext
                .Users
                .Include(r => r.Role)
                .FirstOrDefault(x => x.Email == dto.Email);

            if(user is null)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var correctPassword = passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            
            if (correctPassword == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
