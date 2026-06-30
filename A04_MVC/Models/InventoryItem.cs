/*
 * FILE          : InventoryItem.cs
 * PROJECT       : PROG2126 - Assignment 04 - Home Inventory System
 * PROGRAMMER    : Chase McCash, Shefilkhan Firozkhan, Shreyans Kalpesh
 * FIRST VERSION : 2026-04-12
 * DESCRIPTION   :
 *   This file contains the InventoryItem model class which represents a single
 *   catalogued item in a user's home. Each item is associated with a specific
 *   registered user and stores insurance-relevant details such as item name,
 *   description, category, room/location, purchase price, purchase date, and
 *   serial number.
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A04_MVC.Models
{
    /*
     * NAME    : InventoryItem
     * PURPOSE : The InventoryItem class models a single catalogued item within
     *           the Home Inventory System. It captures all relevant details for
     *           insurance purposes including item identity, room/location within
     *           the residence, financial value, and a unique serial number.
     *           Each item belongs to exactly one AppUser via the UserId foreign
     *           key, ensuring strict per-user data isolation.
     */
    public class InventoryItem
    {
        [Key]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Item name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Item Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50)]
        [Display(Name = "Category")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Room / Location is required.")]
        [StringLength(50)]
        [Display(Name = "Room / Location")]
        public string Room { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Purchase Date")]
        public DateTime? PurchaseDate { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "Purchase Price ($)")]
        [Range(0, 9999999.99, ErrorMessage = "Purchase price must be between $0 and $9,999,999.99.")]
        public decimal? PurchasePrice { get; set; }

        [StringLength(100, ErrorMessage = "Serial number cannot exceed 100 characters.")]
        [Display(Name = "Serial Number")]
        public string? SerialNumber { get; set; }

        // Foreign key — links this item to its owning AppUser
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser? User { get; set; }
    }
}
