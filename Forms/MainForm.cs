// Forms/MainForm.cs
using System;
using System.Data;
using System.Windows.Forms;
using InventoryManagement.Data;

namespace InventoryManagement.Forms;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        LoadProducts();
        this.Text = "Inventory Management";
    }

    private void LoadProducts(string? search = null)
    {
        dgvProducts.DataSource = Database.GetProducts(search);
        CheckLowStock();
    }

    private void CheckLowStock()
    {
        foreach (DataGridViewRow row in dgvProducts.Rows)
        {
            int qty = Convert.ToInt32(row.Cells["Quantity"].Value);
            if (qty <= 5)
                row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        var form = new ProductForm();
        if (form.ShowDialog() == DialogResult.OK)
            LoadProducts();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (dgvProducts.CurrentRow != null)
        {
            int id = Convert.ToInt32(dgvProducts.CurrentRow.Cells["Id"].Value);
            var form = new ProductForm(id);
            if (form.ShowDialog() == DialogResult.OK)
                LoadProducts();
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (dgvProducts.CurrentRow != null)
        {
            int id = Convert.ToInt32(dgvProducts.CurrentRow.Cells["Id"].Value);
            Database.DeleteProduct(id);
            LoadProducts();
        }
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        LoadProducts(txtSearch.Text);
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
        var sfd = new SaveFileDialog { Filter = "CSV|*.csv" };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            var dt = (DataTable)dgvProducts.DataSource;
            using var sw = new System.IO.StreamWriter(sfd.FileName);
            // headers
            sw.WriteLine(string.Join(",", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));
            // rows
            foreach (DataRow row in dt.Rows)
                sw.WriteLine(string.Join(",", row.ItemArray));
            MessageBox.Show("Exported successfully!");
        }
    }
}