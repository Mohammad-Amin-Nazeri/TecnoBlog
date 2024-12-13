using Blog.CoreLayer.Services;
using Blog.CoreLayer.Services.Categories;
using Blog.CoreLayer.Services.Comments;
using Blog.CoreLayer.Services.FileManager;
using Blog.CoreLayer.Services.Posts;
using Blog.CoreLayer.Services.Users;
using Blog.DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddRazorPages()
    ;

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserServices, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddTransient<IPostServies, PostServies>();
builder.Services.AddTransient<IFileManager, FileManager>();
builder.Services.AddTransient<ICommentServies, CommentServies>();
builder.Services.AddTransient<IMainPageService, MainPageService>();

// Add DbContext with SQL Server
builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Configure Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));
});

// Configure Authentication with Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.AccessDeniedPath = "/";
    });

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/ErrorHandler/500");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorHandler/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Configure endpoints
app.MapControllerRoute(
    name: "Default",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
