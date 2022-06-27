using Microsoft.AspNetCore.Mvc;
using BlogAPI.Entities;
using AutoMapper;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Services
{
    public interface IBlogService
    {
        int Create(CreateBlogDto dto);
        IEnumerable<BlogDto> GetAll();
        BlogDto GetById(int id);
        bool Delete(int id);
    }

    public class BlogService : IBlogService
    {
        private readonly BlogDbContext dbContext;
        private readonly IMapper mapper;

        public BlogService(BlogDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
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
        public int Create(CreateBlogDto dto)
        {
            var blog = mapper.Map<Blog>(dto);
            dbContext.Blogs.Add(blog);
            dbContext.SaveChanges();

            return blog.Id;
        }
        public bool Delete(int id)
        {
            var blog = dbContext
                .Blogs
                .FirstOrDefault(e => e.Id == id);

            if (blog == null)
                return false;

            dbContext.Blogs.Remove(blog);
            dbContext.SaveChanges();

            return true;
        }
    }
}
