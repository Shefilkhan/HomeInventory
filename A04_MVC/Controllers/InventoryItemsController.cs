/*
* FILE          : InventoryItemsController.cs
* PROJECT       : PROG2126 - Assignment 04 - Home Inventory System
* PROGRAMMER    : Chase McCash, Shefilkhan Firozkhan, Shreyans Kalpesh
* FIRST VERSION : 2026-04-09
* DESCRIPTION   : This file contains the InventoryItemsController class which provides full
*                 CRUD (Create, Read, Update, Delete) operations for InventoryItem records.
*                 All actions require authentication. Each action reads the authenticated
*                 user's UserId claim to ensure that users can only view and manage items
*                 that belong to their own account.
*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using A04_MVC.Data;
using A04_MVC.Helpers;
using A04_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace A04_MVC.Controllers
{
    [Authorize]
    public class InventoryItemsController : Controller
    {
        private readonly AppDbContext dbContext;

        //Receives the application's Entity Framework Core database
        //context through ASP.NET Core's dependency injection system
        //and stores it for use across all CRUD action methods.
        public InventoryItemsController(AppDbContext context)
        {
            dbContext = context;
            return;
        }

        private int GetCurrentUserId()
        {
            string? userIdClaim = User.FindFirstValue("UserId");
            int userId = 0;

            if (!string.IsNullOrEmpty(userIdClaim))
            {
                userId = int.Parse(userIdClaim);
            }

            return userId;
        }
        //Builds SelectList collections from the InventoryOptions
        //static class and stores them in ViewBag.CategoryList and
        //ViewBag.RoomList for use by the Create and Edit Razor views.
        private void PopulateDropdowns(string? selectedCategory = null, string? selectedRoom = null)
        {
            ViewBag.CategoryList = new SelectList(InventoryOptions.Categories, selectedCategory);
            ViewBag.RoomList = new SelectList(InventoryOptions.Rooms, selectedRoom);
            return;
        }
        //Retrieves all inventory items belonging to the currently
        //authenticated user from the SQL Server database.
        public async Task<IActionResult> Index()
        {
            int userId = GetCurrentUserId();

            List<InventoryItem> userItems = await dbContext.InventoryItems
                .Where(item => item.UserId == userId)
                .OrderBy(item => item.Name)
                .ToListAsync();

            return View(userItems);
        }

        //Retrieves a single inventory item by its primary key and
        //verifies it belongs to the current user before displaying its full details. 
        public async Task<IActionResult> Details(int? id)
        {
            IActionResult result = NotFound();
            int userId = GetCurrentUserId();

            if (id != null)
            {
                InventoryItem? foundItem = await dbContext.InventoryItems
                    .FirstOrDefaultAsync(item => item.ItemId == id && item.UserId == userId);

                if (foundItem != null)
                {
                    result = View(foundItem);
                }
            }

            return result;
        }

        //Displays the empty Create form for adding a new inventory
        //item. Calls PopulateDropdowns to fill the Category and
        //Room select lists in ViewBag before rendering the view.
        [HttpGet]
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        //Processes the submitted Create form. If the model is valid,
        //sets the item's UserId to the current user's id and saves
        //the new InventoryItem to SQL Server, then redirects to Index.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,Description,Category,Room,PurchaseDate,PurchasePrice,SerialNumber")]
            InventoryItem item)
        {
            IActionResult result = RedirectToAction("Index");

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(item.Category, item.Room);
                result = View(item);
            }
            else
            {
                //Assign the item to the currently authenticated user
                item.UserId = GetCurrentUserId();
                dbContext.InventoryItems.Add(item);
                await dbContext.SaveChangesAsync();
            }

            return result;
        }

        //Retrieves an existing inventory item and displays the Edit
        //form pre-populated with the current field values. Verifies
        //the item belongs to the current user. 
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = NotFound();
            int userId = GetCurrentUserId();

            if (id != null)
            {
                InventoryItem? foundItem = await dbContext.InventoryItems
                    .FirstOrDefaultAsync(item => item.ItemId == id && item.UserId == userId);

                if (foundItem != null)
                {
                    PopulateDropdowns(foundItem.Category, foundItem.Room);
                    result = View(foundItem);
                }
            }

            return result;
        }

        //Processes the submitted Edit form. Validates that the route
        //id matches the model's ItemId to detect tampering. If the
        //model is valid, retrieves the tracked entity from the database,
        //verifies ownership, updates only the user-editable fields, and
        //saves. Redirects to Index on success.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("ItemId,Name,Description,Category,Room,PurchaseDate,PurchasePrice,SerialNumber")]
            InventoryItem item)
        {
            IActionResult result = NotFound();
            int userId = GetCurrentUserId();

            if (id == item.ItemId)
            {
                if (!ModelState.IsValid)
                {
                    PopulateDropdowns(item.Category, item.Room);
                    result = View(item);
                }
                else
                {
                    //Load the tracked entity and confirm ownership before modifying
                    InventoryItem? existingItem = await dbContext.InventoryItems
                        .FirstOrDefaultAsync(i => i.ItemId == id && i.UserId == userId);

                    if (existingItem == null)
                    {
                        result = NotFound();
                    }
                    else
                    {
                        //Update only user-editable fields on the EF-tracked entity
                        existingItem.Name = item.Name;
                        existingItem.Description = item.Description;
                        existingItem.Category = item.Category;
                        existingItem.Room = item.Room;
                        existingItem.PurchaseDate = item.PurchaseDate;
                        existingItem.PurchasePrice = item.PurchasePrice;
                        existingItem.SerialNumber = item.SerialNumber;

                        await dbContext.SaveChangesAsync();
                        result = RedirectToAction("Index");
                    }
                }
            }

            return result;
        }

        //Retrieves an inventory item and displays a confirmation
        //page before permanent deletion. Verifies the item exists and belongs to the current user. 
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            IActionResult result = NotFound();
            int userId = GetCurrentUserId();

            if (id != null)
            {
                InventoryItem? foundItem = await dbContext.InventoryItems
                    .FirstOrDefaultAsync(item => item.ItemId == id && item.UserId == userId);

                if (foundItem != null)
                {
                    result = View(foundItem);
                }
            }

            return result;
        }
        //Permanently removes the specified inventory item from the
        //SQL Server database after re-verifying item ownership using
        //the current user's UserId.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int userId = GetCurrentUserId();

            InventoryItem? itemToDelete = await dbContext.InventoryItems
                .FirstOrDefaultAsync(item => item.ItemId == id && item.UserId == userId);

            if (itemToDelete != null)
            {
                dbContext.InventoryItems.Remove(itemToDelete);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
