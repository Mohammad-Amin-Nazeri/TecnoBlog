using Blog.CoreLayer.DTOs.Comments; 
using Blog.CoreLayer.Utilities; 
using Blog.DataLayer.Context; 
using Blog.DataLayer.Entities; 
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; 
using System.Linq; 

namespace Blog.CoreLayer.Services.Comments
{
    public class CommentServies : ICommentServies
    {
        private readonly BlogContext _context; // Database context for interacting with comments

        // Constructor to initialize the service with a database context
        public CommentServies(BlogContext context)
        {
            _context = context;
        }

        // Method to create a new comment
        public OperationResult CreateComment(CreateCommentDto command)
        {
            // Create a new comment entity
            var Comment = new PostComment()
            {
                PostId = command.PostId, // Assign the post ID
                Text = command.Text, // Assign the comment text
                UserId = command.UserId, // Assign the user ID
            };

            _context.Add(Comment); // Add the comment to the database
            _context.SaveChanges(); // Save changes to the database
            return OperationResult.Success(); // Return success result
        }

        // Method to get comments for a specific post
        public List<CommentDto> GetPostComments(int PostId)
        {
            // Query comments for the given post ID, including user details
            return _context.PostCumments
                .Include(c => c.User) // Include user information in the query
                .Where(c => c.PostId == PostId) // Filter comments by post ID
                .Select(coment => new CommentDto() // Map each comment to a DTO
                {
                    PostId = coment.PostId, // Assign the post ID
                    Text = coment.Text, // Assign the comment text
                    UserFullName = coment.User.FullName, // Assign the user's full name
                    CommentId = coment.Id, // Assign the comment ID
                    CreationDate = coment.CreationDate, // Assign the creation date
                })
                .ToList(); // Convert the query result to a list
        }
    }
}
