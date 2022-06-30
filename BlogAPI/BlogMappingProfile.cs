﻿using AutoMapper;
using BlogAPI.Entities;
using BlogAPI.Models;

namespace BlogAPI
{
    public class BlogMappingProfile : Profile
    {
        public BlogMappingProfile()
        {
            CreateMap<Blog, BlogDto>()
                .ForMember(e => e.Comments, c => c.MapFrom(s => s.Comments))
                .ForMember(e => e.Category, c => c.MapFrom(s => s.Category.Name));

            CreateMap<CommentDto, Comment>();

            CreateMap<Comment, CommentDto>();

            CreateMap<CreateBlogDto, Blog>()
                .ForMember(r => r.Category, c => c.MapFrom(dto => new Category() { Name = dto.Category }));

            CreateMap<RegisterUserDto, User>()
                .ForMember(r => r.Role, c => c.MapFrom(dto => new Role() { Id = dto.RoleId }));

        }
    }
}
