using Microsoft.AspNetCore.Mvc;
using BlogAPI.Entities;
using AutoMapper;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using BlogAPI.Services;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/blog")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Blog>> GetAll()
        {
            var blogs = blogService.GetAll();

            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public ActionResult<Blog> Get([FromRoute] int id)
        {
            var blog = blogService.GetById(id);

            return Ok(blog);
        }
        [HttpPost]
        public ActionResult CreatedBlog([FromBody] CreateBlogDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = blogService.Create(dto);

            return Created($"/api/blog/{id}", null);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteBlog([FromRoute] int id)
        {
            var isDeleted = blogService.Delete(id);

            if (!isDeleted)
                return NotFound();

            return Ok();
        }
    }
}
