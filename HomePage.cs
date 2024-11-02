using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Data.SqlClient;

namespace WindowsFormapdemo
{
    public partial class HomePage : UserControl
    {
        string connectstring = @"Data Source=DESKTOP-GD5REG3\SQLEXPRESS;Initial Catalog=QL_chitieu;Integrated Security=True";
        List<Tuple<string, string, DateTime>> transactions = new List<Tuple<string, string, DateTime>>();
        private Timer updateTimer;

        public HomePage()
        {
            InitializeComponent();
            InitializeListView1();
            LoadTransactionData();
            UpdateListView(transactions);
            InitializeListView2();
            LoadAccountData();


            updateTimer = new Timer();
            updateTimer.Interval = 2000; 
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start(); 
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
       
            LoadTransactionData();
        }

        private void btnHientai_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            var filteredTransactions = transactions.Where(t => t.Item3.Date == today.Date).ToList();
            UpdateListView(filteredTransactions);
        }

        private void btnThang_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            var filteredTransactions = transactions.Where(t => t.Item3.Year == today.Year && t.Item3.Month == today.Month).ToList();
            UpdateListView(filteredTransactions);
        }

        private void btnNam_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            var filteredTransactions = transactions.Where(t => t.Item3.Year == today.Year).ToList();
            UpdateListView(filteredTransactions);
        }

        public void btnTransaction_Click(object sender, EventArgs e)
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

        private void LoadTransactionData()
        {
            transactions.Clear();

            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Type, Money,Date FROM Transactions";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string type = reader.GetString(0);
                            string money = reader.GetString(1); 
                            DateTime date = reader.GetDateTime(2);
                            transactions.Add(new Tuple<string, string, DateTime>(type, money, date));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }

            UpdateListView(transactions);
        }

        private void LoadAccountData()
        {
            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT AccountType, Balance FROM Accounts";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string accountType = reader.GetString(0);
                            string balance = reader.IsDBNull(1) ? "0" : reader.GetString(1);

                            ListViewItem item = new ListViewItem(accountType);
                            item.SubItems.Add(balance);
                            listView2.Items.Add(item);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }
        }

        private void UpdateListView(List<Tuple<string, string, DateTime>> transactions)
        {
            listView1.Items.Clear();

            foreach (var transaction in transactions)
            {
                ListViewItem item = new ListViewItem(transaction.Item1);
                item.SubItems.Add(transaction.Item2);
                item.SubItems.Add(transaction.Item3.ToString("dd/MM/yyyy"));
                listView1.Items.Add(item);
            }
        }

        private void InitializeListView1()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Loại", 100);
            listView1.Columns.Add("Số tiền", 150);
            listView1.Columns.Add("Ngày", 120);
        }

        private void InitializeListView2()
        {
            listView2.View = View.Details;
            listView2.Columns.Add("Loại Tài Khoản", 150);
            listView2.Columns.Add("Số Dư", 150);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ((Form1)this.FindForm()).btnHome_Click(sender, e);
        }
    }
}
