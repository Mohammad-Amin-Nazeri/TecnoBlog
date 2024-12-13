using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer.Entities
{
    public class User :BaseEntity
    {

        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<PostComment> PostCumments { get; set; }

    }

    public enum UserRole
    {
        Admin,
        User,
        Writer,

    }

}
