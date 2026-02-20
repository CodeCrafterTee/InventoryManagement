// Forms/LoginForm.cs
using System;
using System.Windows.Forms;

namespace InventoryManagement.Forms;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
        this.Text = "Admin Login";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Text;

        // Simple hardcoded admin credentials (for demo)
        if (username == "admin" && password == "admin123")
        {
            this.Hide();
            var main = new MainForm();
            main.ShowDialog();
            this.Close();
        }
        else
        {
            MessageBox.Show("Invalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}