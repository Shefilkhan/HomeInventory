/*
* FILE          : AccountController.cs
* PROJECT       : PROG2126 - Assignment 04 - Home Inventory System
* PROGRAMMER    : Shreyans Kalpesh, Shefilkhan Fizokhan, Chase McCash
* FIRST VERSION : 2026-04-12
* DESCRIPTION   :
*   This file contains the AccountController class which manages all
*   authentication-related actions for the Home Inventory System. This
*   includes displaying and processing the Registration form, the Login
*   form, and handling Logout. Cookie-based forms authentication is used,
*   following the pattern demonstrated in the course's SimpleFormsAuthDemo.
*/

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A04_MVC.Data;
using A04_MVC.Models;
using A04_MVC.Models.ViewModels;
using A04_MVC.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace A04_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext dbContext;
        public AccountController(AppDbContext context)
        {
            dbContext = context;
            return;
        }

        //Displays the user registration form. If the requesting
        //user is already authenticated, they are redirected to the
        //Home page to prevent unnecessary re-registration.
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            IActionResult result = View();

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                result = RedirectToAction("Index", "Home");
            }

            return result;
        }

        //Processes the submitted registration form. If the model is
        //valid, checks that the chosen username is not already taken in the database.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            IActionResult result = View(registerModel);

            if (ModelState.IsValid)
            {
                //Verify the desired username is not already registered
                bool usernameTaken = await dbContext.AppUsers
                    .AnyAsync(user => user.Username == registerModel.Username);

                if (usernameTaken)
                {
                    ModelState.AddModelError("Username", "That username is already taken. Please choose another.");
                }
                else
                {
                    //Create the new user with a SHA-256 hashed password
                    AppUser newUser = new AppUser
                    {
                        Username = registerModel.Username,
                        PasswordHash = PasswordHasher.HashPassword(registerModel.Password)
                    };

                    dbContext.AppUsers.Add(newUser);
                    await dbContext.SaveChangesAsync();

                    result = RedirectToAction("Login", "Account");
                }
            }

            return result;
        }

        //Displays the login form. The optional returnUrl parameter
        //is saved in ViewData so that after a successful login the
        //user can be redirected back to the page they originally requested 
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //Processes the submitted login credentials. Looks up the user
        //in the SQL Server database by username and verifies the
        //submitted password against the stored SHA-256 hash using passwordHasher. 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel, string? returnUrl = null)
        {
            IActionResult result = View(loginModel);
            string safeReturnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                //Look up user by username in the SQL Server database
                AppUser? foundUser = await dbContext.AppUsers
                    .FirstOrDefaultAsync(user => user.Username == loginModel.Username);

                //Verify password
                if (foundUser == null || !PasswordHasher.VerifyPassword(loginModel.Password, foundUser.PasswordHash))
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
                else
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, foundUser.Username),
                        new Claim("UserId", foundUser.UserId.ToString())
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = System.DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    //Issue the authentication cookie (pattern from SimpleFormsAuthDemo)
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    result = LocalRedirect(safeReturnUrl);
                }
            }

            return result;
        }

        //Signs the currently authenticated user out by deleting
        //the authentication cookie via HttpContext.SignOutAsync,
        //then redirects to the application's Home page.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
