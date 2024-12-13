using Blog.CoreLayer.DTOs.Categories;
using Blog.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Services.Categories
{
    public interface ICategoryService
    {
        OperationResult CreateCategory(CreateCategoryDto Command);
        OperationResult EditCategory(EditCategoryDto Command);
        List<CategoryDto> GetAllCategory();
        List<CategoryDto> GetChildCategories(int parentId);

        CategoryDto GetCategoryBy(int id);
        CategoryDto GetCategoryBy(string slug);
        bool IsSlugExist(string slug);
        OperationResult IsDeleteCategory(DeleteCategoryDto Command);



    }
}
