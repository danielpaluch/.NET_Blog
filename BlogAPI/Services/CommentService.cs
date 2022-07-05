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
    public interface ICommentService
    {
        int Create(int id, CommentDto dto, int userId);
        int Delete(int id, int idOfComment, ClaimsPrincipal user);
    }

    public class CommentService : ICommentService
    {
        private readonly BlogDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IAuthorizationService authorizationService;

        public CommentService(BlogDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
        }
        public int Create(int id, CommentDto dto, int userId)
        {
            var blog = dbContext
                .Blogs
                .Include(r => r.Category)
                .Include(r => r.Comments)
                .FirstOrDefault(e => e.Id == id);

            if (blog == null)
                return -1;

            var comment = mapper.Map<Comment>(dto);

            comment.CreatedById = userId;

            blog.Comments.Add(comment);
            dbContext.SaveChanges();

            return blog.Id;
        }
        public int Delete(int id, int idOfComment, ClaimsPrincipal user)
        {
            var blog = dbContext
                .Blogs
                .Include(r => r.Category)
                .Include(r => r.Comments)
                .FirstOrDefault(e => e.Id == id);

            if (blog == null)
                return -1;


            var commentToDelete = blog.Comments.Where(e => e.Id == idOfComment).FirstOrDefault();

            var authResult = authorizationService.AuthorizeAsync(user, 
                commentToDelete, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if(!authResult.Succeeded)
            {
                throw new ForbidException();
            }

            blog.Comments.Remove(commentToDelete);
            dbContext.SaveChanges();

            return blog.Id;
        }
    }
}
