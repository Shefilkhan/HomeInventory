/*
 * FILE          : InventoryOptions.cs
 * PROJECT       : PROG2126 - Assignment 04 - Home Inventory System
 * PROGRAMMER    : Shefilkhan Fizokhan, Chase McCash, Shreyans Kalpesh
 * FIRST VERSION : 2026-04-12
 * DESCRIPTION   :
 *   This file contains the InventoryOptions helper class which provides
 *   centralized, static predefined lists for item categories and room/
 *   location names used to populate dropdown menus in the Create and
 *   Edit views of the Home Inventory System.
 */

using System.Collections.Generic;

namespace A04_MVC.Helpers
{
    /*
     * NAME    : InventoryOptions
     * PURPOSE : The InventoryOptions static class provides the predefined lists
     *           of item categories and room/location labels used across the
     *           InventoryItems Create and Edit forms. Centralizing these lists
     *           here prevents duplication across the controller and views, and
     *           makes future additions or changes easy to apply in one place.
     */
    public static class InventoryOptions
    {
        public static readonly List<string> Categories = new List<string>
        {
            "Appliances",
            "Art and Collectibles",
            "Books and Media",
            "Clothing and Accessories",
            "Electronics",
            "Furniture",
            "Jewelry",
            "Musical Instruments",
            "Sports and Recreation",
            "Tools and Equipment",
            "Other"
        };

        public static readonly List<string> Rooms = new List<string>
        {
            "Basement",
            "Bathroom",
            "Bedroom",
            "Dining Room",
            "Garage",
            "Home Office",
            "Kitchen",
            "Laundry Room",
            "Living Room",
            "Other"
        };
    }
}
