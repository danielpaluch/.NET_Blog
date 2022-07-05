﻿using Microsoft.AspNetCore.Mvc;
using BlogAPI.Entities;
using AutoMapper;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using BlogAPI.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/blog/{id}/comment")]
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        [HttpPost]
        public ActionResult AddComment([FromRoute] int id, [FromBody] CommentDto comment)
        {
            var userId = int.Parse(User.FindFirst(e => e.Type == ClaimTypes.NameIdentifier).Value);

            var isDone = commentService.Create(id, comment, userId);
            return Ok(isDone);
        }
        [HttpDelete]
        public ActionResult DeleteComment([FromRoute] int id, [FromQuery] int idOfComment)
        {
            var isDone = commentService.Delete(id, idOfComment, User);
            return Ok(isDone);
        }
    }
}
