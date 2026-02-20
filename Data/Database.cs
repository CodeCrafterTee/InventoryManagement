// Data/Database.cs
using System.Data;
using System.Data.SqlClient;
using InventoryManagement.Models;

namespace InventoryManagement.Data;

public static class Database
{
    private static string connectionString = @"Server=localhost;Database=InventoryDB;Trusted_Connection=True;";

    public static SqlConnection GetConnection() => new SqlConnection(connectionString);

    public static DataTable GetProducts(string? search = null)
    {
        using var conn = GetConnection();
        string sql = "SELECT * FROM Products";
        if (!string.IsNullOrEmpty(search))
            sql += " WHERE Name LIKE @search OR Category LIKE @search";

        using var cmd = new SqlCommand(sql, conn);
        if (!string.IsNullOrEmpty(search))
            cmd.Parameters.AddWithValue("@search", $"%{search}%");

        using var adapter = new SqlDataAdapter(cmd);
        var dt = new DataTable();
        adapter.Fill(dt);
        return dt;
    }

    public static void AddProduct(Product p)
    {
        using var conn = GetConnection();
        string sql = "INSERT INTO Products (Name, Category, Quantity, Price) VALUES (@name,@cat,@qty,@price)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@name", p.Name);
        cmd.Parameters.AddWithValue("@cat", p.Category);
        cmd.Parameters.AddWithValue("@qty", p.Quantity);
        cmd.Parameters.AddWithValue("@price", p.Price);
        conn.Open();
        cmd.ExecuteNonQuery();
    }

    public static void UpdateProduct(Product p)
    {
        using var conn = GetConnection();
        string sql = "UPDATE Products SET Name=@name, Category=@cat, Quantity=@qty, Price=@price WHERE Id=@id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@name", p.Name);
        cmd.Parameters.AddWithValue("@cat", p.Category);
        cmd.Parameters.AddWithValue("@qty", p.Quantity);
        cmd.Parameters.AddWithValue("@price", p.Price);
        cmd.Parameters.AddWithValue("@id", p.Id);
        conn.Open();
        cmd.ExecuteNonQuery();
    }

    public static void DeleteProduct(int id)
    {
        using var conn = GetConnection();
        string sql = "DELETE FROM Products WHERE Id=@id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        cmd.ExecuteNonQuery();
    }
}