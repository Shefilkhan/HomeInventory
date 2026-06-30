/*
 * FILE          : HomeController.cs
 * PROJECT       : PROG2126 - Assignment 04 - Home Inventory System
 * PROGRAMMER    : Shreyans Kalpesh, Shefilkhan Fizokhan, Chase McCash
 * FIRST VERSION : 2026-04-12
 * DESCRIPTION   :
 *   This file contains the HomeController class which handles routing for
 *   the application's public landing page, the privacy policy page, and
 *   the error handling page in the Home Inventory System.
 */

using A04_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using A04_MVC.Models;
using System.Diagnostics;

namespace A04_MVC.Controllers
{
    /*
     * NAME    : HomeController
     * PURPOSE : The HomeController class manages the main public-facing pages of
     *           the Home Inventory System. The Index action serves a landing page
     *           accessible to all visitors. The Privacy action serves the privacy
     *           policy. The Error action renders error details using the current
     *           HTTP request trace identifier for diagnostic reference. None of
     *           these actions require authentication.
     */
    public class HomeController : Controller
    {
        //
        // METHOD      : Index
        // DESCRIPTION : Serves the application's public-facing home page. The page
        //               provides a welcome message and prompts unauthenticated users
        //               to log in or register, while authenticated users are directed
        //               to the inventory management section via the navigation bar.
        // PARAMETERS  : none
        // RETURNS     : IActionResult : the rendered Index view
        //
        public IActionResult Index()
        {
            return View();
        }

        //
        // METHOD      : Privacy
        // DESCRIPTION : Serves the application's privacy policy page describing how
        //               user account data and inventory data are stored and protected.
        //               Accessible to all visitors without authentication.
        // PARAMETERS  : none
        // RETURNS     : IActionResult : the rendered Privacy view
        //
        public IActionResult Privacy()
        {
            return View();
        }

        //
        // METHOD      : Error
        // DESCRIPTION : Serves the error page, passing the current request's
        //               Activity ID or TraceIdentifier to the Error view so that
        //               the user can reference it when reporting issues. Response
        //               caching is disabled to ensure error data is always fresh.
        // PARAMETERS  : none
        // RETURNS     : IActionResult : the rendered Error view with request details
        //
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ErrorViewModel errorModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(errorModel);
        }
    }
}
