# Home Inventory System

A secure web-based application designed to catalogue and manage home inventory items for insurance and personal records. This application is structured using the Model-View-Controller (MVC) pattern and built with ASP.NET Core, Entity Framework Core, and SQL Server.

> [!NOTE]
> This application was completed as the final/last semester project for the **Windows Network Programming** course (PROG2126).

---

## 👥 Contributors (Programmers)
* **Shefilkhan Pathan**
* **Chase McCash**
* **Shreyans Kalpesh**

---

## 🚀 Key Features

* **User Authentication**: Secure cookie-based forms authentication (`HomeInventoryAuthCookie`) with user password protection (SHA-256 hashing) via the `AccountController`.
* **Per-User Isolation**: Multi-tenant data structure ensures that authenticated users can only view, add, modify, or delete inventory items belonging to their own account (`UserId` mapping).
* **Comprehensive CRUD Operations**: Fully-featured management of catalogued assets, tracking:
  - Item Name & Description
  - Category (e.g., Furniture, Electronics, etc.)
  - Room/Location (e.g., Living Room, Bedroom, etc.)
  - Purchase Date & Price
  - Serial Number (essential for insurance claims)
* **Robust Form Validation**: Client-side and server-side model state validation to maintain data integrity.
* **SQL Server Database**: Integrated using Entity Framework Core code-first approach with custom `AppDbContext`.

---

## 🛠️ Technology Stack

* **Core Framework**: ASP.NET Core 8.0 MVC (C#)
* **Database Access**: Entity Framework Core
* **Database Engine**: Microsoft SQL Server
* **Authentication**: Cookie-based Authentication Middleware
* **Front-end**: Razor Views, HTML5, CSS3, Bootstrap

---

## 📂 Project Structure

```
HomeInventory/
├── A04_MVC/
│   ├── Controllers/          # Account, Home, and Inventory Controllers
│   ├── Data/                 # DB contexts and migrations
│   ├── Helpers/              # Dropdown helper options
│   ├── Models/               # AppUser and InventoryItem models & viewmodels
│   ├── Views/                # Razor views for accounts and inventory
│   ├── wwwroot/              # Static assets (CSS, JS, images)
│   ├── Program.cs            # App entry point, service registrations, & middleware
│   ├── appsettings.json      # Connection strings and configuration
│   └── A04_MVC.csproj        # MSBuild project file
└── A04_MVC.sln               # Visual Studio Solution file
```

---

## 🏁 Getting Started

### 📋 Prerequisites
* [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [SQL Server Express / LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
* [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended) or VS Code

### ⚙️ Database Configuration
1. Open the file `appsettings.json` in the `A04_MVC` directory.
2. Update the Connection String named `DefaultConnection` to match your SQL Server or LocalDB instance:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HomeInventoryDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```
3. Run Entity Framework Core migrations to create the database schemas:
   ```bash
   dotnet ef database update
   ```

### 💻 Running the App
1. Open terminal at the project root `c:\HomeInventory` (or target directory).
2. Change directory into `A04_MVC`:
   ```bash
   cd A04_MVC
   ```
3. Start the application:
   ```bash
   dotnet run
   ```
4. Open your browser and navigate to the local HTTPS URL shown in the console (usually `https://localhost:7119` or `https://localhost:5001`).
