using Microsoft.AspNetCore.Mvc;
using BlogAPI.Entities;
using AutoMapper;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BlogAPI.Authorization;
using BlogAPI.Exceptions;

namespace BlogAPI.Services
{
    public interface IBlogService
    {
        int Create(CreateBlogDto dto, int userId);
        IEnumerable<BlogDto> GetAll();
        BlogDto GetById(int id);
        bool Delete(int id, ClaimsPrincipal user);
    }

    public class BlogService : IBlogService
    {
        private readonly BlogDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IAuthorizationService authorizationService;

        public BlogService(BlogDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
        }

        public IEnumerable<BlogDto> GetAll()
        {
            var blogs = dbContext
                .Blogs
                .Include(r => r.Category)
                .Include(r => r.Comments)
                .ToList();

            var blogsDto = mapper.Map<List<BlogDto>>(blogs);

            return blogsDto;
        }
        public BlogDto GetById(int id)
        {
            var blog = dbContext
                .Blogs
                .Include(r => r.Category)
                .Include(r => r.Comments)
                .FirstOrDefault(e => e.Id == id);

            var blogDto = mapper.Map<BlogDto>(blog);
            return blogDto;
        }
        public int Create(CreateBlogDto dto, int userId)
        {
            var blog = mapper.Map<Blog>(dto);

            blog.CreatedById = userId;

            dbContext.Blogs.Add(blog);
            dbContext.SaveChanges();

            return blog.Id;
        }
        public bool Delete(int id, ClaimsPrincipal user)
        {

            
            var blog = dbContext
                .Blogs
                .FirstOrDefault(e => e.Id == id);

            if (blog == null)
                return false;

            var authResult = authorizationService.AuthorizeAsync(user, blog, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if(!authResult.Succeeded)
            {
                throw new ForbidException();
            }

            dbContext.Blogs.Remove(blog);
            dbContext.SaveChanges();

            return true;
        }
    }
}
