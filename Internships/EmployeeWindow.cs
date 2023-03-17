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

        private void updateBtn_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow.Index;
            var id = Convert.ToInt32(dataGridView1.Rows[row].Cells[0].Value.ToString());
            var name = dataGridView1.Rows[row].Cells[1].Value.ToString(); 
            var role = dataGridView1.Rows[row].Cells[2].Value.ToString();
            var teamId = dataGridView1.Rows[row].Cells[3].Value.ToString();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter.SelectCommand.Connection = connection;
                    adapter.UpdateCommand = 
                        new SqlCommand(
                            "Update Employees set name = '" + name + "', role = '" + role + "', team_id = " + teamId +
                            " where id = " + id + ";", connection);
                    adapter.Update(data, "Employees");
                    data.Tables["Employees"].Clear();
                    adapter.Fill(data, "Employees");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter.SelectCommand.Connection = connection;
                    adapter.DeleteCommand = new SqlCommand("Delete from Employees where id = " + dataGridView1.CurrentRow.Cells[0].Value.ToString() + ";", connection);
                    adapter.DeleteCommand.ExecuteNonQuery();
                    data.Tables["Employees"].Clear();
                    adapter.Fill(data, "Employees");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}