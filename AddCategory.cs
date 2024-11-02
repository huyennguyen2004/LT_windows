using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormapdemo
{
    public partial class AddCategory : UserControl
    {
        private CategoryPage categoryPage;

        public AddCategory(CategoryPage page)
        {
            InitializeComponent();
            categoryPage = page;
        }
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            string categoryName = txtCategory.Text;
            string categoryType = comboBoxType.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(categoryName))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục.");
                return;
            }

            string connectstring = @"Data Source=DESKTOP-GD5REG3\SQLEXPRESS;Initial Catalog=QL_chitieu;Integrated Security=True";
            string insertQuery = "INSERT INTO Categories (CategoryName, CategoryType) VALUES (@CategoryName, @CategoryType)";

            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                        cmd.Parameters.AddWithValue("@CategoryType", categoryType);

                        cmd.ExecuteNonQuery();

                        if (categoryType == "thunhap")
                        {
                            categoryPage.AddItemToListBox1(categoryName);
                        }
                        else if (categoryType == "chitieu")
                        {
                            categoryPage.AddItemToListBox2(categoryName);
                        }

                        MessageBox.Show("Danh mục đã được thêm thành công.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }

            this.Parent.Controls.Add(categoryPage);
            this.Parent.Controls.Remove(this);
        }

        private void btnLeave_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát mà không lưu không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Parent.Controls.Add(categoryPage);
                this.Parent.Controls.Remove(this);
            }
        }
    }
}
