using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StudentCourse
{
    internal class Program
    {
        static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_student_course;Integrated Security=True;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. View Courses");
                Console.WriteLine("3. Update Course");
                Console.WriteLine("4. Delete Course");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddCourse();
                        break;
                    case 2:
                        ViewCourses();
                        break;
                    case 3:
                        UpdateCourse();
                        break;
                    case 4:
                        DeleteCourse();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddCourse()
        {
            Console.Write("Enter Course ID: ");
            int courseId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Course Name: ");
            string courseName = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Courses (CourseID, CourseName) VALUES (@CourseID, @CourseName)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    cmd.Parameters.AddWithValue("@CourseName", courseName);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Course added successfully.");
                }
            }
        }

        static void ViewCourses()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Courses";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Course ID: {reader["CourseID"]}, Course Name: {reader["CourseName"]}");
                    }
                }
            }
        }

        static void UpdateCourse()
        {
            Console.Write("Enter Course ID to update: ");
            int courseId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter new Course Name: ");
            string newCourseName = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Courses SET CourseName = @CourseName WHERE CourseID = @CourseID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    cmd.Parameters.AddWithValue("@CourseName", newCourseName);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Course updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Course not found.");
                    }
                }
            }
        }

        static void DeleteCourse()
        {
            Console.Write("Enter Course ID to delete: ");
            int courseId = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Courses WHERE CourseID = @CourseID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Course deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Course not found.");
                    }
                }
            }
        }

    }
}
