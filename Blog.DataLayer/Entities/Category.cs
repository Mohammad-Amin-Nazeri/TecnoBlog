using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer.Entities
{
    public class Category : BaseEntity
    {
      
        public string Title { get; set; }
        public string Slug { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescriton { get; set; }
        public int? ParentId { get; set; }
        [InverseProperty("Category")]
        public ICollection<Post> Posts { get; set; }
        [InverseProperty("SubCategory")]
        public ICollection<Post> SubPost { get; set; }

    }
}
