using Blog.CoreLayer.DTOs.Comments;
using Blog.CoreLayer.DTOs.Posts;
using Blog.CoreLayer.Services.Comments;
using Blog.CoreLayer.Services.Posts;
using Blog.CoreLayer.Utlilties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace Blog.web.Pages
{
    public class PostModel : PageModel
    {
        private readonly IPostServies _postServies;
        private readonly ICommentServies _commentServies;
        public PostModel(IPostServies postServies , ICommentServies commentServies)
        {
            _postServies = postServies;
            _commentServies = commentServies;
        }

        public PostDto Post { get; set; }
        [Required]
        [BindProperty]
        public string Text { get; set; }
        [BindProperty]
        public int PostId { get; set; }

        public List<CommentDto> Comments { get; set; }
        public List<PostDto> RelatedPost { get; set; }
        
        public IActionResult OnGet(string slug)
        {
            Post = _postServies.GetPostBySlug(slug);
            if (Post == null)
                return NotFound();

            Comments = _commentServies.GetPostComments(Post.PostId);
            RelatedPost = _postServies.GetRelatedPost(Post.CategoryId);
            
            _postServies.IncreaseVisit(Post.PostId);

            return Page();

        }

        public IActionResult OnPost(string slug) 
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("Post", new { slug });

            if (!ModelState.IsValid)
            {
                Post = _postServies.GetPostBySlug(slug);
                Comments = _commentServies.GetPostComments(Post.PostId);
                RelatedPost = _postServies.GetRelatedPost(Post.SubCategoryId ?? Post.CategoryId);
                return Page();
            }
            _commentServies.CreateComment(new CoreLayer.DTOs.Comments.CreateCommentDto()
            {
                PostId = PostId,
                Text = Text,
                UserId = User.GetUserId(),
            });
            return RedirectToPage("Post", new { slug });

        }
    }
}
