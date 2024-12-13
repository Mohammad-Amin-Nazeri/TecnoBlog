using Blog.CoreLayer.DTOs.Users; // Import DTOs for users
using Blog.DataLayer.Context; // Import database context
using Blog.DataLayer.Entities; // Import data entities
using Blog.CoreLayer.Utilities; // Import utilities
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.CoreLayer.Services.Users
{
    // Service class for managing users
    public class UserService : IUserServices
    {
        private readonly BlogContext _context; // Database context

        // Constructor to initialize the database context
        public UserService(BlogContext context)
        {
            _context = context;
        }

        // Edit user information
        public OperationResult EditUser(EditUserDto command)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id == command.UserId);
            if (user == null)
                return OperationResult.NotFound();

            user.FullName = command.FullName;
            user.Role = command.Role;
            _context.SaveChanges();
            return OperationResult.Success();
        }

        // Register a new user
        public OperationResult RegisterUser(UserRegisterDto registerDto)
        {
            if (_context.Users.Any(u => u.UserName == registerDto.UserName))
                return OperationResult.Error("Username already exists");

            var passwordHash = registerDto.Password.EncodeToMd5();
            _context.Users.Add(new User()
            {
                FullName = registerDto.FullName,
                IsDelete = false,
                UserName = registerDto.UserName,
                Role = UserRole.User,
                CreationDate = DateTime.Now,
                Password = passwordHash
            });
            _context.SaveChanges();
            return OperationResult.Success();
        }

        // Log in a user
        public UserDto LoginUser(LoginUserDto loginDto)
        {
            var passwordHashed = loginDto.Password.EncodeToMd5();
            var user = _context.Users.FirstOrDefault(u => u.UserName == loginDto.UserName && u.Password == passwordHashed);
            if (user == null)
                return null;

            return new UserDto()
            {
                FullName = user.FullName,
                Password = user.Password,
                Role = user.Role,
                UserName = user.UserName,
                RegesterDate = user.CreationDate,
                UserId = user.Id
            };
        }

        // Get user details by ID
        public UserDto GetUserById(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return null;

            return new UserDto()
            {
                FullName = user.FullName,
                Password = user.Password,
                Role = user.Role,
                UserName = user.UserName,
                RegesterDate = user.CreationDate,
                UserId = user.Id
            };
        }

        // Get users with pagination
        public UserFilterDto GetUsersByFilter(int pageId, int take)
        {
            var users = _context.Users.OrderByDescending(d => d.Id).Where(c => !c.IsDelete);

            var skip = (pageId - 1) * take; // Calculate items to skip
            var model = new UserFilterDto()
            {
                Users = users.Skip(skip).Take(take).Select(user => new UserDto()
                {
                    FullName = user.FullName,
                    Password = user.Password,
                    Role = user.Role,
                    UserName = user.UserName,
                    RegesterDate = user.CreationDate,
                    UserId = user.Id
                }).ToList()
            };
            model.GeneratePaging(users, take, pageId); // Generate pagination data
            return model;
        }
    }
}
