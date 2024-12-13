using Blog.CoreLayer.DTOs;
using Blog.CoreLayer.Mappers;
using Blog.CoreLayer.Services.Posts;
using Blog.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Services
{
    public interface IMainPageService
    {
        MainPageDto GetData();
    }

    public class MainPageService : IMainPageService
    {
        private readonly BlogContext _context;
        public MainPageService( BlogContext context)
        {
            _context = context;
        }

        public MainPageDto GetData()
        {
            var categories = _context.categories.OrderByDescending(d=>d.Id).Take(6).Include(c=> c.Posts).Include(c=>c.SubPost).Select(category=>new MainpageCategoryDto()
            {
                Title = category.Title,
                Slug = category.Slug,
                PostChild = category.Posts.Count + category.SubPost.Count,
                IsMainCategory = category.ParentId == null,
            }).ToList();

            var spcialPosts = _context.Posts.OrderByDescending(d=>d.Id).Include(c=>c.Category).Include(c=>c.SubCategory).Where(r=>r.IsSpecial).Take(4).Select(post=> PostMapper.MapDto(post)).ToList();

            var latestPost = _context.Posts.Include(c=>c.Category).Include(c=>c.SubCategory).OrderByDescending(d=>d.Id).Take(6).Select(post=> PostMapper.MapDto(post)).ToList();
            return new MainPageDto()
            {
                Categories = categories,
                LatestPosts = latestPost,
                SpecialPosts = spcialPosts
            };
           
        }
    }
}
