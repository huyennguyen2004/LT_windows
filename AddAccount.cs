using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace WindowsFormapdemo
{
    public partial class AddAccount : UserControl
    {
        private AccPage _accPage;
        string connectstring = @"Data Source=DESKTOP-GD5REG3\SQLEXPRESS;Initial Catalog=QL_chitieu;Integrated Security=True";
        public AddAccount(AccPage accPage)
        {   
            InitializeComponent();
            _accPage = accPage; 
        }
        private void SaveAccountToDatabase(string accountType, string accountName, string balance)
        {
            string query = "INSERT INTO Accounts (AccountType, Balance) VALUES (@AccountType, @Balance)";

            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountType", accountType);
                    cmd.Parameters.AddWithValue("@Balance", balance);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Có lỗi xảy ra: {ex.Message}");
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản.");
                return;
            }

            string accType = comboBoxType.SelectedItem.ToString();
            string accountName = txtAccountName.Text;
            string balance = txtBalance.Text;

            if (string.IsNullOrWhiteSpace(accountName) || string.IsNullOrWhiteSpace(balance))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản và số dư.");
                return;
            }

            SaveAccountToDatabase(accType, accountName, balance);

            _accPage.LoadAccountData(accType);

            this.Parent.Controls.Add(_accPage);
            this.Parent.Controls.Remove(this);
        }


        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Add(_accPage);
            this.Parent.Controls.Remove(this);
        }
    }
}
