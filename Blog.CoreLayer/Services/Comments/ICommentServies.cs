using Blog.CoreLayer.DTOs.Categories;
using Blog.CoreLayer.DTOs.Comments;
using Blog.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Services.Comments
{
    public interface ICommentServies
    {
        OperationResult CreateComment(CreateCommentDto command);
        List<CommentDto> GetPostComments(int PostId);
    }
}
