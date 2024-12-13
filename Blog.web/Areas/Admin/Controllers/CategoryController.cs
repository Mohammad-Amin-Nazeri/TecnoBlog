using Blog.CoreLayer.DTOs.Categories;
using Blog.CoreLayer.Services.Categories;
using Blog.web.Areas.Admin.Models.Categories;
using Blog.Web.Areas.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Blog.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : AdminControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View(_categoryService.GetAllCategory());
        }
        [Route("/admin/category/add/{parentId?}")]

        public IActionResult Add(int? parentId)
        {
            return View();
        }

        [HttpPost("/admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId, CreateCategoryViewModel createViewModel)
        {
            createViewModel.ParentId = parentId;
            var result = _categoryService.CreateCategory(createViewModel.MapDto());
            if (result.Status != CoreLayer.Utilities.OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(createViewModel.Slug), result.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryBy(id);
            if (category == null)
                return RedirectToAction("Index");
            var model = new EditCategoryViewModel()
            {
                Slug = category.Slug,
                MetaTag = category.MetaTag,
                Title = category.Title,
                MetaDescription = category.MetaDescription
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditCategoryViewModel editViewModel)
        {
            var result = _categoryService.EditCategory(new CoreLayer.DTOs.Categories.EditCategoryDto()
            {
                MetaTag = editViewModel.MetaTag,
                Title = editViewModel.Title,
                MetaDescription = editViewModel.MetaDescription,
                Id = editViewModel.Id,
                Slug = editViewModel.Slug,

            });
            if (result.Status != CoreLayer.Utilities.OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(editViewModel.Slug), result.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult GetChildCategories(int parentId)
        {
            var categoy = _categoryService.GetChildCategories(parentId);

            return new JsonResult(categoy);
        }

       
    }
}
