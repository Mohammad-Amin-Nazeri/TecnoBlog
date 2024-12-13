using Blog.CoreLayer.DTOs.Categories;
using System;

namespace Blog.CoreLayer.DTOs.Posts
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string UserName { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Keyword { get; set; }
        public string SmallDescription { get; set; }

        public string Description { get; set; }
        public string ImageName { get; set; }
        public int Visit { get; set; }
        public bool IsSpecial { get; set; }

        public DateTime CreationDate { get; set; }
        public CategoryDto  category { get; set; }

        public CategoryDto SubCategory { get; set; }

    }
}
