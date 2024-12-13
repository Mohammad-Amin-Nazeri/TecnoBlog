using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Utlilties
{
    public class Directories
    {
        public static string PostImage = "wwwroot/images/posts";
        public static string PostContentImage = "wwwroot/images/posts/content";
        public static string GetPostImage(string imageName) => $"{PostImage.Replace("wwwroot", "")}/{imageName}";
        public static string GetPostContentImage(string imageName) => $"{PostContentImage.Replace("wwwroot", "")}/{imageName}";

    }
}
