using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.DTOs.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescription { get; set; }
    }
}
