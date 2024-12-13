using System.ComponentModel.DataAnnotations;

namespace Blog.web.Areas.Admin.Models.Categories
{
    public class EditCategoryViewModel
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "وارد کردن {0} فیلد اجباری است")]
        public string Title { get; set; }
        [Display(Name = "Slug")]
        [Required(ErrorMessage = "وارد کردن {0} فیلد اجباری است")]
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        [Display(Name = "با - از هم جدا کنید")]
        public string MetaTag { get; set; }
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }
    }
}
