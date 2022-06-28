using Microsoft.AspNetCore.Mvc;
using BlogAPI.Entities;
using AutoMapper;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Services
{
    public interface ICommentService
    {
        int Create(int id, CommentDto dto);
        int Delete(int id, int idOfComment);
    }

    public class CommentService : ICommentService
    {
        private readonly BlogDbContext dbContext;
        private readonly IMapper mapper;

        public CommentService(BlogDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public int Create(int id, CommentDto dto)
        {
            var blog = dbContext
                .Blogs
                .Include(r => r.Category)
                .Include(r => r.Comments)
                .FirstOrDefault(e => e.Id == id);

            if (blog == null)
                return -1;

            var comment = mapper.Map<Comment>(dto);
            blog.Comments.Add(comment);
            dbContext.SaveChanges();

            return blog.Id;
        }
        public int Delete(int id, int idOfComment)
        {
            var blog = dbContext
                .Blogs
                .Include(r => r.Category)
                .Include(r => r.Comments)
                .FirstOrDefault(e => e.Id == id);

            if (blog == null)
                return -1;

            var commentToDelete = blog.Comments.Where(e => e.Id == idOfComment).FirstOrDefault();
            blog.Comments.Remove(commentToDelete);

            dbContext.SaveChanges();

            return blog.Id;
        }
    }
}
