﻿using Blog.CoreLayer.DTOs.Posts;
using Blog.CoreLayer.Utlilties;
using Blog.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Mappers
{
    public class PostMapper
    {
        public static Post MapCreateDtoToPost(CreatePostDto dto)
        {
            return new Post()
            {
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                Slug = dto.Slug,
                Title = dto.Title,
                UserId = dto.UserId,
                Visit = 0,
                SubCategoryId =dto.SubCategoryId,
                IsDelete = false,
                IsSpecial = dto.IsSpecial,
                Keyword = dto.Keyword,
                SmallDescription = dto.SmallDescription,
                
            };

        }
        public static PostDto MapDto(Post post)
        {
           
            return new PostDto()
            {
                Description = post.Description,
                CategoryId = post.CategoryId,
                Slug = post.Slug,
                Title = post.Title,
                UserName = post.User?.FullName,
                Visit = post.Visit,
                CreationDate = post.CreationDate,
                category = post.Category == null ? null : CategoryMapper.Map(post.Category),
                ImageName = post.ImageName,
                PostId = post.Id,
                SubCategoryId=post.SubCategoryId,
                SubCategory = post.SubCategory  == null ? null : CategoryMapper.Map(post.SubCategory),
                IsSpecial = post.IsSpecial,
                Keyword= post.Keyword,
                SmallDescription = post.SmallDescription,

                

            };

        }
        public static Post MapEditDtoToPost(EditPostDto editDto, Post post)
        {
            post.Description = editDto.Description;
            post.Title = editDto.Title;
            post.CategoryId = editDto.CategoryId;
            post.Slug = editDto.Slug.ToSlug();
            post.SubCategoryId = editDto.SubCategoryId;
            post.IsSpecial = editDto.IsSpecial;
            post.Keyword = editDto.Keyword;
            post.SmallDescription = editDto.SmallDescription;
            return post;
        }
    }
   
}
