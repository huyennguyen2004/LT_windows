using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormapdemo
{
    public partial class TransactionPage : UserControl
    {
        string connectionString = @"Data Source=DESKTOP-GD5REG3\SQLEXPRESS;Initial Catalog=QL_chitieu;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataGridTextBox dtb;

        public TransactionPage()
        {
            InitializeComponent();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            var form = this.FindForm() as Form1;
            if (form != null)
            {
                form.btnHome_Click(sender, e);
            }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string type = comboBoxType.SelectedItem?.ToString() ?? "";
            string category = comboBoxCategory.SelectedItem?.ToString() ?? "";
            string source = comboBoxSource.SelectedItem?.ToString() ?? "";
            string money = txtMoney.Text;
            string description = txtDescription.Text;
            DateTime selectedDate = dateTimePicker1.Value;
            int hour = (int)numericUpDownHour.Value;
            int minute = (int)numericUpDownMinute.Value;

            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(source) || string.IsNullOrEmpty(money))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO Transactions (Type, Money, Category, Source, Description, Date, Hour, Minute) " +
                                   "VALUES (@Type, @Money, @Category, @Source, @Description, @Date, @Hour, @Minute); " +
                                   "SELECT SCOPE_IDENTITY();"; 

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Type", type);
                        command.Parameters.AddWithValue("@Money", money);
                        command.Parameters.AddWithValue("@Category", category);
                        command.Parameters.AddWithValue("@Source", source);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@Date", selectedDate);
                        command.Parameters.AddWithValue("@Hour", hour);
                        command.Parameters.AddWithValue("@Minute", minute);

            
                        int newId = Convert.ToInt32(command.ExecuteScalar());

                   
                        dataGridView1.Rows.Add(newId, type, money, category, source, description, $"{selectedDate.ToShortDateString()}", hour, minute);

                        MessageBox.Show("Lưu giao dịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa mục này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        var idValue = row.Cells[0].Value;
                        if (idValue != null && int.TryParse(idValue.ToString(), out int Id))
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                try
                                {
                                    connection.Open();

                                    string query = "DELETE FROM Transactions WHERE Id = @Id"; 

                                    using (SqlCommand command = new SqlCommand(query, connection))
                                    {
                                        command.Parameters.AddWithValue("@Id", Id);
                                        command.ExecuteNonQuery();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Lỗi khi xóa dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            dataGridView1.Rows.Remove(row);
                        }
                        else
                        {
                            MessageBox.Show("Giá trị ID không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
