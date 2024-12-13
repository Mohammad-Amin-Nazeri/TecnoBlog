using Blog.CoreLayer.Utilities;
using System.Collections.Generic;


namespace Blog.CoreLayer.DTOs.Users
{
    public class UserFilterDto:BasePagination
    {
        public List<UserDto> Users { get; set; }
    }

}