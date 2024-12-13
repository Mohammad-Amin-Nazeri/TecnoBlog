using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Blog.web.Areas.Admin.Models.Posts
{
    public class CreatePostViewModel
    {
        [Display(Name ="انتخاب دسته بندی")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public int CategoryID { get; set; }
        [Display(Name = "انتخاب زیر بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? SubCategoryId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "Slug")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Slug { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [UIHint("")]
        public string Description { get; set; }
       
        public IFormFile ImageFile { get; set; }
        [Display(Name = "پست ویژه")]
        public bool IsSpecial { get; set; }
        [Display(Name = "کلمات کلیدی ")]
        public string Keyword { get; set; }
        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "لطفا کمتر از 200 کاراکتر وارد کنید")]
        [MinLength(50, ErrorMessage = "لطفا بیشتر از 50 کاراکتر وارد کنید")]
        public string SmallDescription { get; set; }



    }

    public class EditPostViewModel
    {
        [Display(Name = "انتخاب دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryID { get; set; }
        [Display(Name = "انتخاب زیر بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? SubCategoryId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "Slug")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Slug { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [UIHint("")]
        public string Description { get; set; }
        [Display(Name = "عکس")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "پست ویژه")]
        public bool IsSpecial { get; set; }

        [Display(Name = "کلمات کلیدی ")]
        public string Keyword { get; set; }
        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "لطفا کمتر از 200 کاراکتر وارد کنید")]
        [MinLength(50, ErrorMessage = "لطفا بیشتر از 50 کاراکتر وارد کنید")]
        public string SmallDescription { get; set; }

    }
}
