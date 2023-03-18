using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var teamId = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells[0].Value);
                RegisterEmployee window = new RegisterEmployee(teamId);
                window.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var teamId = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells[0].Value);
            var window = new EmployeeWindow(teamId);
            window.Show();
        }
    }
}