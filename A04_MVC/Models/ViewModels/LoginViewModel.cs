/*
 * FILE          : LoginViewModel.cs
 * PROJECT       : PROG2126 - Assignment 04 - Home Inventory System
 * PROGRAMMER    : Shefilkhan Fizokhan, Chase McCash, Shreyans Kalpesh
 * FIRST VERSION : 2026-04-12
 * DESCRIPTION   :
 *   This file contains the LoginViewModel class which carries login form
 *   data (username and password) submitted by the user from the Login view
 *   to the AccountController for processing and authentication.
 */

using System.ComponentModel.DataAnnotations;

namespace A04_MVC.Models.ViewModels
{
    /*
     * NAME    : LoginViewModel
     * PURPOSE : The LoginViewModel class carries the user-submitted login
     *           credentials from the Login form to the AccountController.
     *           Data annotations enforce required fields and provide user-
     *           friendly validation error messages that are displayed in
     *           the Login view via the validation tag helpers.
     */
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
    }
}
