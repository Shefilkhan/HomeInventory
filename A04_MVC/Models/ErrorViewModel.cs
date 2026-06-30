/*
* FILE          : ErrorViewModel.cs
* PROJECT       : PROG2126 - Assignment 04 - Home Inventory System
* PROGRAMMER    : Chase McCash, Shreyans Kalpesh, Shefilkhan Fizokhan
* FIRST VERSION : 2026-04-12
* DESCRIPTION   :
*   This file contains the ErrorViewModel class used to pass the current
*   HTTP request identifier from the HomeController to the Error view,
*   allowing users and developers to reference the request for diagnostics.
*/

namespace A04_MVC.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
