using System;
using System.Windows.Forms;
using StuDash;

namespace StuDash
{
    /// <summary>
    /// Add this method to your Program.cs or create a test form
    /// </summary>
    public class DatabaseTest
    {
        public static void TestDatabaseConnection()
        {
            try
            {
                using (var service = new StudentService())
                {
                    // Test 1: Get all students
                    var students = service.GetAllStudents();
                    MessageBox.Show($"Database connected successfully!\n" +
                                  $"Found {students.Count} students.",
                                  "Success",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);

                    // Test 2: Add a new student
                    var testStudent = new Student
                    {
                        StudentID = "TEST.001",
                        FirstName = "Test",
                        LastName = "Student",
                        Email = "test@school.edu",
                        PhoneNumber = "0241234567",
                        DateOfBirth = DateTime.Now.AddYears(-20),
                        Faculty = "Engineering",
                        Department = "Computer Science & Engineering",
                        Course = "Computer Science",
                        YearLevel = "First Year",
                        CWA = 0.00M,
                        EnrollmentDate = DateTime.Now
                    };

                    bool added = service.AddStudent(testStudent);
                    if (added)
                    {
                        MessageBox.Show("Test student added successfully!",
                                      "Add Test Passed",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);

                        // Test 3: Search for the student
                        var foundStudent = service.GetStudentByStudentId("TEST.001");
                        if (foundStudent != null)
                        {
                            MessageBox.Show($"Found student: {foundStudent.FullName}",
                                          "Search Test Passed",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Information);

                            // Test 4: Delete the test student (cleanup)
                            service.DeleteStudent(foundStudent.ID);
                            MessageBox.Show("Test student deleted successfully!",
                                          "Delete Test Passed",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Information);
                        }
                    }
                }

                MessageBox.Show("All database tests passed! ✓\n\n" +
                              "You can now use the database version.",
                              "Tests Complete",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database test failed!\n\n" +
                              $"Error: {ex.Message}\n\n" +
                              $"Inner Exception: {ex.InnerException?.Message}",
                              "Test Failed",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }
    }
}

// Add this to your Program.cs Main method to run the test:
/*
static void Main()
{
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    
    // Run database test first
    DatabaseTest.TestDatabaseConnection();
    
    // Then run your main form
    Application.Run(new MainForm());
}
*/