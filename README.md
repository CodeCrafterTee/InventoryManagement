# Inventory Management System

A C# WinForms desktop app for managing products, tracking stock, and exporting data.

---

## Features

- Admin login
- Add, edit, delete products
- Low stock alerts (highlighted in red)
- Search functionality
- Export inventory to CSV

---

## Technologies

- C# WinForms
- SQL Server
- OOP principles
- CRUD operations
- File handling (CSV export)

---

## Getting Started

1. Open `InventoryManagement.sln` in Visual Studio
2. Update connection string in `Database.cs` to match your SQL Server
3. Create a table:

```sql
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Category NVARCHAR(100),
    Quantity INT,
    Price DECIMAL(18,2)
);