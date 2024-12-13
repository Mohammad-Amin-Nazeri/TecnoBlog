
using Blog.CoreLayer.DTOs.Posts;
using Blog.CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.web.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IPostServies _postServies;
        public SearchModel(IPostServies postServies)
        {
            _postServies = postServies;
        }
        public PostFilterDto Filter { get; set; }
        public void OnGet(int pageId=1 , string categorySlug=null ,string q = null)
        {
            Filter = _postServies.GetPostsByFilter(new PostFilterParms()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 6,
                Title = q
            });

        }

        public IActionResult OngetPagination(int pageId = 1, string categorySlug = null, string q = null)
        {
            var model = _postServies.GetPostsByFilter(new PostFilterParms()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 6,
                Title = q
            });
            return Partial("_SearchView", model);
        }
    }
}
