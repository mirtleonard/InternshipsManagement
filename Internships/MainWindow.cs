using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Internships
{
    public partial class MainWindow : Form
    {
        string connectionString =
            @"Server=LEONARD\SQLEXPRESS02;Database=Internships;Integrated Security=true;TrustServerCertificate=true;";

        DataSet ds = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter();

        public MainWindow()
        {
            InitializeComponent();
            Load += Main_Load;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter.SelectCommand = new SqlCommand("SELECT * FROM Teams;", connection);
                    adapter.Fill(ds, "Teams");
                    dataGridView1.DataSource = ds.Tables["Teams"];
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        { 
            var row = e.RowIndex;
            var info = dataGridView1.Rows[row].Cells[0].Value;
            var window = new EmployeeWindow(Convert.ToInt32(info.ToString()));
            window.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                adapter.SelectCommand.Connection = connection;
                dataGridView1.DataSource = ds.Tables["Teams"];
                ds.Tables["Teams"].Clear();
                adapter.Fill(ds, "Teams");
            }
        }
    }
}