using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Internships
{
    public partial class EmployeeWindow : Form
    {
        
        string connectionString =
            @"Server=LEONARD\SQLEXPRESS02;Database=Internships;Integrated Security=true;TrustServerCertificate=true;";
        private int teamId;
        private DataSet data = new DataSet();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        public EmployeeWindow(int teamId)
        {
            InitializeComponent();
            Load += EmployeeWindow_Load;
            this.teamId = teamId;
        }
        
        private void EmployeeWindow_Load(object sender, EventArgs e)
        {
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter.SelectCommand = new SqlCommand("SELECT * FROM Employees where team_id = " + teamId + ";", connection);
                    adapter.Fill(data, "Employees");
                    dataGridView1.DataSource = data.Tables["Employees"];
                                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}