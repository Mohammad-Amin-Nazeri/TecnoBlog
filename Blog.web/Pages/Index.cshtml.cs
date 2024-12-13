using Blog.CoreLayer.DTOs;
using Blog.CoreLayer.DTOs.Posts;
using Blog.CoreLayer.Services;
using Blog.CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.web.Pages
{

    public class IndexModel : PageModel
    {
        private readonly IPostServies _postService;
        private readonly IMainPageService _mainPageService;
        public IndexModel(IPostServies postService, IMainPageService mainPageService)
        {
            _postService = postService;
            _mainPageService = mainPageService;
        }

        public MainPageDto MainPageData { get; set; }

        public void OnGet()
        {
            MainPageData = _mainPageService.GetData();
        }

        public IActionResult OnGetLatestPosts(string categorySlug)
        {
            var filterDto = _postService.GetPostsByFilter(new PostFilterParms()
            {
                CategorySlug = categorySlug,
                PageId = 1,
                Take = 6
            });
            return Partial("_LatestPosts", filterDto.Posts);
        }
        public IActionResult OnGetPopularPost()
        {
            return Partial("_PopularPosts", _postService.GetPopularPost());
        }
        public IActionResult OnGetPopularPostPage()
        {
            return Partial("_PopularPostEndPage", _postService.GetPopularPost());
        }
    }
}
