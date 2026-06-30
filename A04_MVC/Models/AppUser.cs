/*
 * FILE          : AppUser.cs
 * PROJECT       : PROG2126 - Assignment 04 - Home Inventory System
 * PROGRAMMER    : Shefilkhan Firzkhan, Shreyans Kalpesh, Chase McCash
 * FIRST VERSION : 2026-04-12
 * DESCRIPTION   :
 *   This file contains the AppUser model class which represents a registered
 *   user of the Home Inventory System. It stores login credentials and serves
 *   as the parent entity for all InventoryItems that belong to that user.
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace A04_MVC.Models
{
    /*
     * NAME    : AppUser
     * PURPOSE : The AppUser class models a registered user of the Home Inventory
     *           application. It stores the user's login credentials (username and
     *           SHA-256 hashed password) and maintains a navigation property to
     *           all InventoryItems belonging to this user. Authentication is
     *           cookie-based and enforced through the AccountController. Only
     *           authenticated users may access or manage their own item data.
     */
    public class AppUser
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        // Navigation property: one user owns many inventory items
        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }
}
