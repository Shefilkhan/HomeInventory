/*
 * FILE          : Program.cs
 * PROJECT       : PROG2126 - Assignment 04 - Home Inventory System
 * PROGRAMMER    : Shefilkhan pathan, Chase McCash, Shreyans Kalpesh
 * FIRST VERSION : 2026-04-12
 * DESCRIPTION   :
 *   This file is the application entry point for the Home Inventory System.
 *   It registers all required services including the Entity Framework Core
 *   SQL Server database context, cookie-based forms authentication, and
 *   configures the full ASP.NET Core MVC middleware pipeline for the app.
 */

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using A04_MVC.Data;
using System;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Register MVC controllers and Razor views
builder.Services.AddControllersWithViews();

// Register EF Core database context with SQL Server connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure cookie-based forms authentication
// Pattern based on SimpleFormsAuthDemo provided in course material
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Login";
        options.Cookie.Name = "HomeInventoryAuthCookie";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

WebApplication app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Authentication must be registered before Authorization in the pipeline
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
