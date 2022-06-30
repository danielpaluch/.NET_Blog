using Microsoft.AspNetCore.Mvc;
using BlogAPI.Entities;
using AutoMapper;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using BlogAPI.Services;

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

        public UserService(BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
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
                Password = dto.Password,
                Email = dto.Email,
                RoleId = dto.RoleId
            };

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
