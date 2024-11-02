using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormapdemo
{
    public partial class Login : UserControl
    { 
        string connectstring = @"Data Source=DESKTOP-GD5REG3\SQLEXPRESS;Initial Catalog=QL_chitieu;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataGridTextBox dtb;

        private Form1 parentForm;
        public event EventHandler LoginSuccess;
        public bool IsAuthenticated { get; private set; }

        public Login(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent; 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = txtEmailOrUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectstring))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Login WHERE usernameOrEmail = @usernameOrEmail AND password = @password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usernameOrEmail", usernameOrEmail);
                        cmd.Parameters.AddWithValue("@password", password);

                        int result = (int)cmd.ExecuteScalar();

                        if (result > 0)
                        {
                            IsAuthenticated = true;
                            MessageBox.Show("Đăng nhập thành công!");
                            LoginSuccess?.Invoke(this, EventArgs.Empty);

                        }
                        else
                        {
                            MessageBox.Show("Thông tin đăng nhập sai.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Signup signupPage = new Signup(parentForm);
            signupPage.Dock = DockStyle.Fill;
            parentForm.Controls.Clear();
            parentForm.Controls.Add(signupPage);
        }
    }

}
