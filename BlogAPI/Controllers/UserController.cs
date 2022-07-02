using Microsoft.AspNetCore.Mvc;
using BlogAPI.Entities;
using AutoMapper;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using BlogAPI.Services;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            var users = userService.GetUsers();

            return Ok(users);
        }
        [HttpPost("register")]
        public ActionResult CreateUser([FromBody] RegisterUserDto dto)
        {
            userService.CreateUser(dto);

            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteUser([FromRoute] int id)
        {
            userService.DeleteUser(id);

            return Ok();
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = userService.GenerateToken(dto);

            return Ok(token);
        }
    }
}
