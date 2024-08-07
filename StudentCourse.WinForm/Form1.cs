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

namespace StudentCourse.WinForm
{
    public partial class Form1 : Form
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_student_course;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Courses (CourseID, CourseName) VALUES (@CourseID, @CourseName)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", CourseIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@CourseName", CourseNameTextBox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Course added successfully.");
                    ClearFields();
                }
            }
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Courses";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    CoursesGridView.DataSource = dt;
                }
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Courses SET CourseName = @CourseName WHERE CourseID = @CourseID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", CourseIDTextBox.Text);
                    cmd.Parameters.AddWithValue("@CourseName", CourseNameTextBox.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Course updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Course not found.");
                    }
                    ClearFields();
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Courses WHERE CourseID = @CourseID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", CourseIDTextBox.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Course deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Course not found.");
                    }
                    ClearFields();
                }
            }
        }

        private void ClearFields()
        {
            CourseIDTextBox.Text = "";
            CourseNameTextBox.Text = "";
        }
    }
}
