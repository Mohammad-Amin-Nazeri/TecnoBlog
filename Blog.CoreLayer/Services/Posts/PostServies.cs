using Blog.CoreLayer.DTOs.Posts; // DTOs related to post operations
using Blog.CoreLayer.Mappers; // For mapping between DTOs and entities
using Blog.CoreLayer.Services.FileManager; // Service for file operations
using Blog.CoreLayer.Utilities; // Utility classes like OperationResult
using Blog.CoreLayer.Utlilties;
using Blog.DataLayer.Context; // Database context
using Blog.DataLayer.Entities; // Entity definitions
using Microsoft.EntityFrameworkCore; // For database queries and relationships
using System.Collections.Generic; // For List<T>
using System.Linq; // For LINQ operations
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database; // Logger category for debugging

namespace Blog.CoreLayer.Services.Posts
{
    public class PostServies : IPostServies
    {
        private readonly BlogContext _context; // Database context for accessing data
        private readonly IFileManager _fileManager; // Service for file management

        // Constructor to inject dependencies
        public PostServies(BlogContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        // Create a new post
        public OperationResult CreatePost(CreatePostDto command)
        {
            // Check if the slug already exists
            if (IsSlugExist(command.Slug))
                return OperationResult.Error("Slug Is Exist");

            // Check if the image file is null
            if (command.ImageFile == null)
                return OperationResult.Error();

            // Map the command DTO to a post entity
            var post = PostMapper.MapCreateDtoToPost(command);

            // Save the image file and assign its name to the post
            post.ImageName = _fileManager.SaveFile(command.ImageFile, Directories.PostImage);
            _context.Posts.Add(post); // Add the post to the database
            _context.SaveChanges(); // Save changes to the database
            return OperationResult.Success();
        }

        // Edit an existing post
        public OperationResult EditPost(EditPostDto command)
        {
            // Find the post by its ID
            var post = _context.Posts.FirstOrDefault(c => c.Id == command.PostId);
            var oldImage = post.ImageName; // Keep track of the old image

            if (post == null)
                return OperationResult.NotFound();

            // Check if the new slug is different and already exists
            if (command.Slug.ToSlug() != post.Slug)
                if (IsSlugExist(command.Slug))
                    return OperationResult.Error("Slug Is Exist");

            // Map the updated values to the post
            PostMapper.MapEditDtoToPost(command, post);

            // Handle new image upload
            if (command.ImageFile != null)
                post.ImageName = _fileManager.SaveFile(command.ImageFile, Directories.PostImage);

            _context.SaveChanges(); // Save the updated post

            // Delete the old image if a new one was uploaded
            if (command.ImageFile != null)
                _fileManager.DeleteFile(oldImage, Directories.PostImage);

            return OperationResult.Success();
        }

        // Get a list of popular posts
        public List<PostDto> GetPopularPost()
        {
            return _context.Posts.Include(c => c.User)
                .OrderByDescending(d => d.Visit)
                .Take(6) // Take the top 6 posts by visits
                .Select(post => PostMapper.MapDto(post))
                .ToList();
        }

        // Get a post by its ID
        public PostDto GetPostById(int PostId)
        {
            var post = _context.Posts.Include(c => c.SubCategory)
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == PostId);

            return PostMapper.MapDto(post); // Map and return the post
        }

        // Get a post by its slug
        public PostDto GetPostBySlug(string Slug)
        {
            var post = _context.Posts.Include(c => c.SubCategory)
                .Include(c => c.Category)
                .Include(c => c.User)
                .FirstOrDefault(c => c.Slug == Slug);

            if (post == null)
                return null;

            return PostMapper.MapDto(post);
        }

        // Get posts based on filter parameters
        public PostFilterDto GetPostsByFilter(PostFilterParms filterParams)
        {
            var result = _context.Posts
                .Include(d => d.Category)
                .Include(d => d.SubCategory)
                .OrderByDescending(d => d.CreationDate)
                .AsQueryable();

            // Filter by category slug
            if (!string.IsNullOrWhiteSpace(filterParams.CategorySlug))
                result = result.Where(r => r.Category.Slug == filterParams.CategorySlug || r.SubCategory.Slug == filterParams.CategorySlug);

            // Filter by title
            if (!string.IsNullOrWhiteSpace(filterParams.Title))
                result = result.Where(r => r.Title.Contains(filterParams.Title));

            // Pagination calculations
            var skip = (filterParams.PageId - 1) * filterParams.Take;
            var pageCount = result.Count() / filterParams.Take;

            // Create a filter result DTO
            var model = new PostFilterDto()
            {
                Posts = result.Skip(skip).Take(filterParams.Take)
                     .Select(post => PostMapper.MapDto(post)).ToList(),
                FilterParms = filterParams,
            };
            model.GeneratePaging(result, filterParams.Take, filterParams.PageId);

            return model;
        }

        // Get related posts by category ID
        public List<PostDto> GetRelatedPost(int categoryId)
        {
            return _context.Posts
                .OrderByDescending(d => d.CreationDate)
                .Where(r => r.CategoryId == categoryId || r.SubCategoryId == categoryId)
                .Take(6) // Take top 6 related posts
                .Select(post => PostMapper.MapDto(post))
                .ToList();
        }

        // Increment the visit count for a post
        public void IncreaseVisit(int postId)
        {
            var post = _context.Posts.First(p => p.Id == postId);
            post.Visit += 1; // Increment visit count
            _context.SaveChanges(); // Save changes
        }

        // Check if a slug exists
        public bool IsSlugExist(string slug)
        {
            return _context.Posts.Any(c => c.Slug == slug.ToSlug());
        }
    }
}
