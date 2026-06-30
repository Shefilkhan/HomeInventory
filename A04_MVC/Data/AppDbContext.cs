/*
 * FILE          : AppDbContext.cs
 * PROJECT       : PROG2126 - Assignment 04 - Home Inventory System
 * PROGRAMMER    : Chase McCash, Shefilkhan Firozkhan, Shreyans Kalpesh
 * FIRST VERSION : 2026-04-09
 * DESCRIPTION   : This file contains the AppDbContext class which is the Entity Framework
 *   Core database context for the Home Inventory System. It manages the SQL
 *   Server connection and exposes DbSet properties for the AppUser and
 *   InventoryItem entities, and configures the relationship between them.
 */

using Microsoft.EntityFrameworkCore;
using A04_MVC.Models;

namespace A04_MVC.Data
{
    /*
     * NAME    : AppDbContext
     * PURPOSE : The AppDbContext class serves as the Entity Framework Core
     *           session with the SQL Server database. It exposes DbSet
     *           collections for AppUser and InventoryItem. The OnModelCreating
     *           override configures the one-to-many relationship between users
     *           and items with cascade delete, and enforces a unique index on
     *           the AppUser.Username column to prevent duplicate accounts.
     *           This class is registered with the ASP.NET Core dependency
     *           injection container in Program.cs.
     */
    public class AppDbContext : DbContext
    {
        //
        // METHOD      : AppDbContext (constructor)
        // DESCRIPTION : Passes the EF Core database context options up to the
        //               base DbContext class. The options object contains the SQL
        //               Server connection string resolved from appsettings.json
        //               via the ASP.NET Core dependency injection system.
        // PARAMETERS  : DbContextOptions<AppDbContext> options : EF Core options
        // RETURNS     : N/A (constructor)
        //
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            return;
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }

        //
        // METHOD      : OnModelCreating
        // DESCRIPTION : Configures the EF Core entity model relationships and
        //               database constraints. Establishes a one-to-many relationship
        //               between AppUser and InventoryItem with cascade delete so that
        //               all items are removed when their owning user is deleted.
        //               Also applies a unique index on AppUser.Username to prevent
        //               duplicate username registrations at the database level.
        // PARAMETERS  : ModelBuilder modelBuilder : EF Core fluent API model builder
        // RETURNS     : void
        //
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many: one AppUser has many InventoryItems
            modelBuilder.Entity<InventoryItem>()
                .HasOne(item => item.User)
                .WithMany(user => user.InventoryItems)
                .HasForeignKey(item => item.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Enforce unique username at the database level
            modelBuilder.Entity<AppUser>()
                .HasIndex(user => user.Username)
                .IsUnique();

            return;
        }
    }
}
