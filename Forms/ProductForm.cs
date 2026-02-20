// Forms/ProductForm.cs
using System;
using System.Windows.Forms;
using InventoryManagement.Models;
using InventoryManagement.Data;

namespace InventoryManagement.Forms;

public partial class ProductForm : Form
{
    private int? _productId;

    public ProductForm(int? productId = null)
    {
        InitializeComponent();
        _productId = productId;

        this.Text = productId == null ? "Add Product" : "Edit Product";
        if (productId != null)
            LoadProduct();
    }

    private void LoadProduct()
    {
        var dt = Database.GetProducts();
        var row = dt.Select($"Id = {_productId}")[0];
        txtName.Text = row["Name"].ToString();
        txtCategory.Text = row["Category"].ToString();
        numQty.Value = Convert.ToInt32(row["Quantity"]);
        numPrice.Value = Convert.ToDecimal(row["Price"]);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var product = new Product
        {
            Id = _productId ?? 0,
            Name = txtName.Text,
            Category = txtCategory.Text,
            Quantity = (int)numQty.Value,
            Price = numPrice.Value
        };

        if (_productId == null)
            Database.AddProduct(product);
        else
            Database.UpdateProduct(product);

        DialogResult = DialogResult.OK;
    }
}