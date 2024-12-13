using Blog.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.DTOs.Posts
{
    public class PostFilterDto : BasePagination
    {
       

        public List<PostDto> Posts { get; set; }
        public PostFilterParms FilterParms { get; set; }

    }
    public class PostFilterParms
    {
        public string Title { get; set; }
        public string CategorySlug { get; set; }
        public int PageId { get; set; }
        public int Take { get; set; }
    }
}
