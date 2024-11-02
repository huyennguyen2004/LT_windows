using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace WindowsFormapdemo
{
    public partial class AccPage : UserControl
    {
        string connectionString = @"Data Source=DESKTOP-GD5REG3\SQLEXPRESS;Initial Catalog=QL_chitieu;Integrated Security=True";
        public AccPage()
        {
            InitializeComponent();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ((Form1)this.FindForm()).btnHome_Click(sender, e);
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            ((Form1)this.FindForm()).btnTransaction_Click(sender, e);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ((Form1)this.FindForm()).btnReport_Click(sender, e);
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            ((Form1)this.FindForm()).btnCategory_Click(sender, e);
        }

        private void btnAcc_Click(object sender, EventArgs e)
        {
            ((Form1)this.FindForm()).btnAcc_Click(sender, e);
        }

        private void btnOther_Click(object sender, EventArgs e)
        {
            ((Form1)this.FindForm()).btnOther_Click(sender, e);
        }
        public void LoadAccountData(string accountType)
        {
            listView1.Items.Clear();

            string query = "SELECT AccountType, Balance FROM Accounts  WHERE AccountType = @AccountType";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số cho câu truy vấn
                    cmd.Parameters.AddWithValue("@AccountType", accountType);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string type = reader["AccountType"].ToString();
                            string balance = reader["Balance"].ToString();
                            listView1.Items.Add(new ListViewItem($"{type} - Số dư: {balance}"));
                        }
                    }
                }
            }
        }

        private void btnBank_Click(object sender, EventArgs e)
        {
            LoadAccountData("Tai khoan ngan hang");
        }

        private void btnSaveMoney_Click(object sender, EventArgs e)
        {
            LoadAccountData("Vi");
        }

        private void btnAccumulate_Click(object sender, EventArgs e)
        {
            LoadAccountData("Quy");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddAccount addAccount = new AddAccount(this);
            addAccount.Dock = DockStyle.Fill;
            this.Parent.Controls.Add(addAccount);
            this.Parent.Controls.Remove(this); 
        }

        public void AddItemToListBox1(string item)
        {
            listView1.Items.Add(new ListViewItem(item)); 
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
