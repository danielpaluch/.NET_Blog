using Microsoft.AspNetCore.Mvc;
using BlogAPI.Entities;
using AutoMapper;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using BlogAPI.Services;
using Microsoft.AspNetCore.Identity;

namespace BlogAPI.Services
{
    public interface IUserService
    {
        User CreateUser(RegisterUserDto dto);
        int DeleteUser(int id);
        IEnumerable<User> GetUsers();
    }

    public class UserService : IUserService
    {
        private readonly BlogDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasher;

        public UserService(BlogDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
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

    }
}
