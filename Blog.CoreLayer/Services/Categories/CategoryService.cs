using Blog.CoreLayer.DTOs.Categories;
using Blog.CoreLayer.Mappers; 
using Blog.CoreLayer.Utilities; 
using Blog.CoreLayer.Utlilties;
using Blog.DataLayer.Context; 
using Blog.DataLayer.Entities; 
using System.Collections.Generic; 
using System.Linq; 

namespace Blog.CoreLayer.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogContext _Context; // Database context for interacting with categories

        // Constructor to initialize the service with a database context
        public CategoryService(BlogContext context)
        {
            _Context = context;
        }

        // Method to create a new category
        public OperationResult CreateCategory(CreateCategoryDto Command)
        {
            // Check if the provided slug already exists
            if (IsSlugExist(Command.Slug))
                return OperationResult.Error("Slug Is Exist"); // Return error if slug exists

            // Create a new category entity
            var Category = new Category()
            {
                Title = Command.Title,
                IsDelete = false, // New categories are not marked as deleted
                ParentId = Command.ParentId,
                Slug = Command.Slug.ToSlug(), // Convert slug to a standardized format
                MetaTag = Command.MetaTag,
                MetaDescriton = Command.MetaDescription,
            };

            // Add the category to the database
            _Context.categories.Add(Category);
            _Context.SaveChanges(); // Save changes to the database
            return OperationResult.Success(); // Return success result
        }

        // Method to edit an existing category
        public OperationResult EditCategory(EditCategoryDto Command)
        {
            // Find the category by its ID
            var category = _Context.categories.FirstOrDefault(c => c.Id == Command.Id);

            if (category == null)
                return OperationResult.NotFound(); // Return not found if category doesn't exist

            // Check if the new slug is different and already exists
            if (Command.Slug.ToSlug() != category.Slug)
                if (IsSlugExist(Command.Slug))
                    return OperationResult.Error("Slug Is Exist"); // Return error if slug exists

            // Update the category fields
            category.MetaDescriton = Command.MetaDescription;
            category.Slug = Command.Slug.ToSlug();
            category.Title = Command.Title;
            category.MetaTag = Command.MetaTag;

            _Context.SaveChanges(); // Save changes to the database
            return OperationResult.Success(); // Return success result
        }

        // Method to retrieve all categories that are not deleted
        public List<CategoryDto> GetAllCategory()
        {
            return _Context.categories
                .Where(p => p.IsDelete != true) // Filter out deleted categories
                .Select(category => CategoryMapper.Map(category)) // Map entities to DTOs
                .ToList();
        }

        // Method to get a category by its ID
        public CategoryDto GetCategoryBy(int id)
        {
            var category = _Context.categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return null; // Return null if category doesn't exist

            return CategoryMapper.Map(category); // Map entity to DTO
        }

        // Method to get a category by its slug
        public CategoryDto GetCategoryBy(string slug)
        {
            var category = _Context.categories.FirstOrDefault(c => c.Slug == slug);
            if (category == null) return null; // Return null if category doesn't exist

            return CategoryMapper.Map(category); // Map entity to DTO
        }

        // Method to retrieve child categories for a given parent category
        public List<CategoryDto> GetChildCategories(int parentId)
        {
            return _Context.categories
                .Where(r => r.ParentId == parentId) // Filter categories by parent ID
                .Select(category => CategoryMapper.Map(category)) // Map entities to DTOs
                .ToList();
        }

        // Method to mark a category as deleted
        public OperationResult IsDeleteCategory(DeleteCategoryDto Command)
        {
            var category = _Context.categories.FirstOrDefault(c => c.Id == Command.Id);

            if (category == null)
            {
                return OperationResult.NotFound(); // Return not found if category doesn't exist
            }

            category.IsDelete = true; // Mark the category as deleted

            _Context.SaveChanges(); // Save changes to the database
            return OperationResult.Success(); // Return success result
        }

        // Method to check if a slug already exists
        public bool IsSlugExist(string slug)
        {
            return _Context.categories.Any(c => c.Slug == slug.ToSlug()); // Check for slug existence
        }
    }
}
