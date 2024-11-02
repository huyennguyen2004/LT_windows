using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;

namespace WindowsFormapdemo
{

    public partial class ReportPage : UserControl
    {
        string connectstring = @"Data Source=DESKTOP-GD5REG3\SQLEXPRESS;Initial Catalog=QL_chitieu;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataGridView dtb;
        public ReportPage()
        {
            InitializeComponent();
            panel2.Visible = false;
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
            panel2.Visible = true;
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadReportData(string reportType)
        {
            using (SqlConnection conn = new SqlConnection(connectstring))
            {
                conn.Open();
                string query;
                if (reportType == "Tong Quan")
                {
                    query = "SELECT Thunhap,Chitieu,Tong FROM Reports WHERE ReportType = 'Tong Quan'";
                }
                else if (reportType == "Chi Tieu")
                {
                    query = "SELECT Danhmuc, Chitieu AS 'Số Tiền' FROM Reports WHERE ReportType = 'Chi Tieu'";
                }
                else if (reportType == "Su Kien")
                {
                    query = "SELECT Sukien, Thunhap, Chitieu FROM Reports WHERE ReportType = 'Su Kien'";
                }
                else
                {
                    throw new ArgumentException("Invalid report type");
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            LoadReportData("Tong Quan");
            UpdatePieChart(10000000, 5000000);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadReportData("Chi Tieu");
            UpdatePieChart(0, 7000000);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            LoadReportData("Su Kien");
            UpdatePieChart(13000000, 5000000);
        }

        private void UpdatePieChart(decimal income, decimal expense)
        {
            chart1.Series.Clear(); 

            Series series = new Series
            {
                Name = "Financial",
                Color = Color.Blue,
                IsValueShownAsLabel = true,
                ChartType = SeriesChartType.Pie
            };
            series.Points.AddXY("Thu Nhap", income);
            series.Points.AddXY("Chi Tieu", expense);

         
            chart1.Series.Add(series);
            chart1.Invalidate();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
