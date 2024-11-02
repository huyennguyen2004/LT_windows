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
    public partial class CategoryPage : UserControl
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adt;

        private Panel panelAddCategory;

        public CategoryPage()
        {
            InitializeComponent();
            LoadData();
            InitializePanel();
        }
        private void InitializePanel()
        {
            this.panelAddCategory = new System.Windows.Forms.Panel();
            this.panelAddCategory.Location = new System.Drawing.Point(0, 0); 
            this.panelAddCategory.Size = new System.Drawing.Size(400, 300); 
            this.Controls.Add(this.panelAddCategory);
        }
        private void LoadData()
        {
            string connectstring = @"Data Source=DESKTOP-GD5REG3\SQLEXPRESS;Initial Catalog=QL_chitieu;Integrated Security=True";
            string query = "SELECT CategoryName, CategoryType FROM Categories";

            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categoryName = reader["CategoryName"].ToString();
                                string categoryType = reader["CategoryType"].ToString();

                                Console.WriteLine($"CategoryName: {categoryName}, CategoryType: {categoryType}");
                                if (categoryType == "thunhap")
                                {
                                    listBox1.Items.Add(categoryName);
                                }
                                else if (categoryType == "chitieu")
                                {
                                    listBox2.Items.Add(categoryName);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
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

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = listBox1.SelectedItem.ToString();
            MessageBox.Show("Bạn đã chọn loại thu nhập: " + selectedItem);
        }

        public void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = listBox2.SelectedItem.ToString();
            MessageBox.Show("Bạn đã chọn loại chi tiêu: " + selectedItem);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCategory addCategory = new AddCategory(this);
            this.Parent.Controls.Add(addCategory);
            this.Parent.Controls.Remove(this);
        }
        public void AddItemToListBox1(string item)
        {
            listBox1.Items.Add(item);
        }

        public void AddItemToListBox2(string item)
        {
            listBox2.Items.Add(item);
        }

    }
}
