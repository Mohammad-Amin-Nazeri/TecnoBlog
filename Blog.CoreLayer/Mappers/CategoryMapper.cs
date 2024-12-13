using Blog.CoreLayer.DTOs.Categories;
using Blog.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Mappers
{
    public class CategoryMapper
    {
        public static CategoryDto Map( Category category)
        {
            return new CategoryDto()
            {
                MetaDescription = category.MetaDescriton,
                MetaTag = category.MetaTag,
                Title = category.Title,
                Slug = category.Slug,
                ParentId = category.ParentId,
                Id = category.Id
            };
        } 
    }
}
