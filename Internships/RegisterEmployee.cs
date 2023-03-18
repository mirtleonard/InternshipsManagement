using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Internships
{
     public partial class RegisterEmployee : Form
    {
        string connectionString =
            @"Server=LEONARD\SQLEXPRESS02;Database=Internships;Integrated Security=true;TrustServerCertificate=true;";
        private int teamId;
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataSet ds = new DataSet();
           
        public RegisterEmployee(int teamId)
        {
            InitializeComponent();
            this.teamId = teamId;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string role = textBox2.Text;
            if (name == "" || role == "")
            {
                MessageBox.Show("Please fill all the fields"); 
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new  SqlCommand(
                        "Insert into Employees(name, role, team_id) values('" + name + "', '" + role + "'," + teamId +
                        ");", connection);
                    command.ExecuteNonQuery();
                    this.Close();
                    MessageBox.Show("Employee added successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }
    }
}