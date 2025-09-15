using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//GALAGAR, GLYZEL PART 1
namespace Galagar
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=CISCOLAB0025\\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentDBDataSet.Students' table. You can move, or remove it, as needed.
            this.studentsTableAdapter.Fill(this.studentDBDataSet.Students);

        }

        private void save_button_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Students VALUES(@FirstName,@LastName,@Age,@Course)",conn);
                command.Parameters.AddWithValue("@FirstName",firstName_textBox.Text);
                command.Parameters.AddWithValue("@LastName", lastName_textBox.Text);
                command.Parameters.AddWithValue("@Age", Convert.ToInt32(age_textBox.Text));
                command.Parameters.AddWithValue("@Course", course_textBox.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Student Added");
            }
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("UPDATE Students SET FirstName = @FirstName, LastName = @LastName, Age = @Age , Course = @Course WHERE StudentID = @StudentID", conn);
                    command.Parameters.AddWithValue("@FirstName", firstName_textBox.Text);
                    command.Parameters.AddWithValue("@LastName", lastName_textBox.Text);
                    command.Parameters.AddWithValue("@Age", Convert.ToInt32(age_textBox.Text));
                    command.Parameters.AddWithValue("@Course", course_textBox.Text);
                    command.Parameters.AddWithValue("@StudentID", student_id.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Student Edited");
                }
            }catch(Exception)
            {
                MessageBox.Show("Student does not exist");
            }

            

        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Students  WHERE StudentID = @StudentID", conn);
                    command.Parameters.AddWithValue("@StudentID", student_id.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Student Deleted");
                }
            }catch(Exception)
            {
                MessageBox.Show("Student does not exist");
            }
           
            
        }

        private void load_button_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Students", conn);
                DataTable tb = new DataTable();
                adapter.Fill(tb);
                sql_dataView.DataSource = tb;
            }
            ClearAll();
        }
        private void ClearAll()
        {
            firstName_textBox.Text = string.Empty;
            lastName_textBox.Text = string.Empty;
            age_textBox.Text = string.Empty ;
            course_textBox.Text = string.Empty ;
            student_id.Text = string.Empty;
        }
    }
}
