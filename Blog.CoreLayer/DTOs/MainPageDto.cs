using Blog.CoreLayer.DTOs.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.DTOs
{
    public class MainPageDto
    {
        public List<PostDto> LatestPosts { get; set; }
        public List<PostDto> SpecialPosts { get; set; }
        public List<MainpageCategoryDto> Categories { get; set; }
    }
    public class MainpageCategoryDto
    {
        public bool IsMainCategory { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public int PostChild { get; set; }
    }
}
