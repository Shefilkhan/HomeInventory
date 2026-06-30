/*
 * FILE          : RegisterViewModel.cs
 * PROJECT       : PROG2126 - Assignment 04 - Home Inventory System
 * PROGRAMMER    : Shefilkhan Fizokhan, Chase McCash, Shreyans Kalpesh
 * FIRST VERSION : 2026-04-12
 * DESCRIPTION   :
 *   This file contains the RegisterViewModel class which carries registration
 *   form data (username, password, confirm password) from the Register view
 *   to the AccountController for new user creation and validation.
 */

using System.ComponentModel.DataAnnotations;

namespace A04_MVC.Models.ViewModels
{
    /*
     * NAME    : RegisterViewModel
     * PURPOSE : The RegisterViewModel class carries the user-submitted registration
     *           data from the Register form to the AccountController. The [Compare]
     *           annotation on ConfirmPassword enforces that both password fields match
     *           before the form passes model validation. StringLength enforces a
     *           minimum password length of 6 characters for basic security.
     */
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
