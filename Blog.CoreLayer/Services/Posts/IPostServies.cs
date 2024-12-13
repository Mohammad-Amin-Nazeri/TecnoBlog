using Blog.CoreLayer.DTOs.Posts;
using Blog.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Blog.CoreLayer.Services.Posts
{
    public interface IPostServies
    {
        OperationResult CreatePost(CreatePostDto command);
        OperationResult EditPost(EditPostDto command);
        PostDto GetPostById(int PostId);
        PostDto GetPostBySlug(string Slug);
        PostFilterDto GetPostsByFilter(PostFilterParms filterParms);
        bool IsSlugExist(string slug);
        List<PostDto> GetRelatedPost(int groupId);
        List<PostDto> GetPopularPost(); 
        void IncreaseVisit(int postId);


    }
}
