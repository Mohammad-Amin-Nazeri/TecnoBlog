using Blog.CoreLayer.DTOs.Posts;
using Blog.CoreLayer.Services.Posts;
using Blog.CoreLayer.Utlilties;
using Blog.web.Areas.Admin.Models.Posts;
using Blog.Web.Areas.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PostController : AdminControllerBase
    {
        private readonly IPostServies _postServies;
        public PostController(IPostServies postServies)
        {
            _postServies = postServies;
        }
        public IActionResult Index(int pageId = 1, string title = "", string categorySlug = "")
        {
            var param = new PostFilterParms()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 5,
                Title = title
            };
            var model = _postServies.GetPostsByFilter(param);
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreatePostViewModel createPostViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = _postServies.CreatePost(new CreatePostDto()
            {
                CategoryId = createPostViewModel.CategoryID,
                Description = createPostViewModel.Description,
                ImageFile = createPostViewModel.ImageFile,
                Slug = createPostViewModel.Slug.ToSlug(),
                SubCategoryId = createPostViewModel.SubCategoryId == 0 ? null : createPostViewModel.SubCategoryId,
                Title = createPostViewModel.Title,
                IsSpecial = createPostViewModel.IsSpecial,
                UserId = User.GetUserId(),
                Keyword = createPostViewModel.Keyword,
                SmallDescription = createPostViewModel.SmallDescription,
                
            });
            if (result.Status != CoreLayer.Utilities.OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(CreatePostViewModel.Slug), result.Message);
                return View(createPostViewModel);
            };
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var post = _postServies.GetPostById(id);
            if (post == null)
                return RedirectToAction("Index");

            var model = new EditPostViewModel()
            {
                CategoryID = post.CategoryId,
                Description = post.Description,
                Slug = post.Slug,
                SubCategoryId = post.SubCategoryId,
                Title = post.Title,
                IsSpecial = post.IsSpecial,
                Keyword = post.Keyword,
                SmallDescription = post.SmallDescription,

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( int id , EditPostViewModel editPostViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = _postServies.EditPost(new EditPostDto()
            {
                CategoryId = editPostViewModel.CategoryID,
                Description = editPostViewModel.Description,
                ImageFile = editPostViewModel.ImageFile,
                Slug = editPostViewModel.Slug.ToSlug(),
                SubCategoryId = editPostViewModel.SubCategoryId == 0 ? null : editPostViewModel.SubCategoryId,
                Title = editPostViewModel.Title,
                PostId = id,
                IsSpecial=editPostViewModel.IsSpecial,
                Keyword=editPostViewModel.Keyword,
                SmallDescription = editPostViewModel.SmallDescription,
            });
            if (result.Status != CoreLayer.Utilities.OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(CreatePostViewModel.Slug), result.Message);
                return View(editPostViewModel);
            }
            return RedirectToAction("Index");
        }
    }
}
