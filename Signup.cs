using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormapdemo
{
    public partial class Signup : UserControl
    {
        string connectstring = @"Data Source=DESKTOP-GD5REG3\SQLEXPRESS;Initial Catalog=QL_chitieu;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataGridTextBox dtb;

        private Form1 parentForm;
        public event EventHandler SignupSuccess;
        public bool IsAuthenticated { get; private set; }
        public Signup(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            string email = txtEmailOrUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectstring = @"Data Source=DESKTOP-GD5REG3\SQLEXPRESS;Initial Catalog=QL_chitieu;Integrated Security=True";
            string insertQuery = "INSERT INTO Login (usernameOrEmail, password, confirmPassword) VALUES (@usernameOrEmail, @password, @confirmPassword)";

            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@usernameOrEmail", email);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@confirmPassword", confirmPassword);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Đăng ký thành công!.Chuyển tới trang đăng nhập");

                        Login loginPage = new Login(parentForm);
                        loginPage.Dock = DockStyle.Fill;
                        parentForm.Controls.Clear();
                        parentForm.Controls.Add(loginPage);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          Login loginPage = new Login(parentForm);
            loginPage.Dock = DockStyle.Fill;
            parentForm.Controls.Clear();
            parentForm.Controls.Add(loginPage);
        }
    }
}

