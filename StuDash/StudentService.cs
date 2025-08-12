using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StuDash
{
    internal class StudentService
    {
        private List<Student> _students;
        private readonly string _dataFilePath;
        private int _nextId;

        public StudentService()
        {
            _students = new List<Student>();
            // Save data to the application's directory
            _dataFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                        "StudentManagementSystem", "students.json");

            // Ensure directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(_dataFilePath));

            LoadData();
        }

        #region CRUD Operations

        /// <summary>
        /// Get all students
        /// </summary>
        public List<Student> GetAllStudents()
        {
            return _students.ToList(); // Return a copy to prevent external modification
        }

        /// <summary>
        /// Get student by ID
        /// </summary>
        public Student GetStudentById(int id)
        {
            return _students.FirstOrDefault(s => s.ID == id);
        }

        /// <summary>
        /// Get student by Student ID
        /// </summary>
        public Student GetStudentByStudentId(string studentId)
        {
            return _students.FirstOrDefault(s => s.StudentID.Equals(studentId, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Add a new student
        /// </summary>
        public bool AddStudent(Student student)
        {
            try
            {
                // Check if Student ID already exists
                if (GetStudentByStudentId(student.StudentID) != null)
                {
                    throw new ArgumentException($"Student ID '{student.StudentID}' already exists.");
                }

                // Assign new ID
                student.ID = _nextId++;

                // Add to list
                _students.Add(student);

                // Save to file
                SaveData();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to add student: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Update existing student
        /// </summary>
        public bool UpdateStudent(Student updatedStudent)
        {
            try
            {
                var existingStudent = GetStudentById(updatedStudent.ID);
                if (existingStudent == null)
                {
                    throw new ArgumentException($"Student with ID {updatedStudent.ID} not found.");
                }

                // Check if new StudentID conflicts with another student
                var studentWithSameId = GetStudentByStudentId(updatedStudent.StudentID);
                if (studentWithSameId != null && studentWithSameId.ID != updatedStudent.ID)
                {
                    throw new ArgumentException($"Student ID '{updatedStudent.StudentID}' already exists.");
                }

                // Update properties
                existingStudent.StudentID = updatedStudent.StudentID;
                existingStudent.FirstName = updatedStudent.FirstName;
                existingStudent.LastName = updatedStudent.LastName;
                existingStudent.Email = updatedStudent.Email;
                existingStudent.PhoneNumber = updatedStudent.PhoneNumber;
                existingStudent.DateOfBirth = updatedStudent.DateOfBirth;
                existingStudent.Course = updatedStudent.Course;
                existingStudent.Faculty = updatedStudent.Faculty;
                existingStudent.Department = updatedStudent.Department;
                existingStudent.YearLevel = updatedStudent.YearLevel;
                existingStudent.CWA = updatedStudent.CWA;
                existingStudent.EnrollmentDate = updatedStudent.EnrollmentDate;

                // Save to file
                SaveData();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to update student: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Delete student by ID
        /// </summary>
        public bool DeleteStudent(int id)
        {
            try
            {
                var student = GetStudentById(id);
                if (student == null)
                {
                    throw new ArgumentException($"Student with ID {id} not found.");
                }

                _students.Remove(student);
                SaveData();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to delete student: {ex.Message}", ex);
            }
        }

        #endregion

        #region Search and Filter

        /// <summary>
        /// Search students by various criteria
        /// </summary>
        public List<Student> SearchStudents(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAllStudents();

            searchTerm = searchTerm.ToLower();

            return _students.Where(s =>
                s.StudentID.ToLower().Contains(searchTerm) ||
                s.FirstName.ToLower().Contains(searchTerm) ||
                s.LastName.ToLower().Contains(searchTerm) ||
                s.Email.ToLower().Contains(searchTerm) ||
                s.Course.ToLower().Contains(searchTerm) ||
                s.Faculty.ToLower().Contains(searchTerm) ||
                s.Department.ToLower().Contains(searchTerm)
            ).ToList();
        }

        /// <summary>
        /// Get students by faculty
        /// </summary>
        public List<Student> GetStudentsByFaculty(string faculty)
        {
            return _students.Where(s => s.Faculty.Equals(faculty, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Get students by year level
        /// </summary>
        public List<Student> GetStudentsByYearLevel(string yearLevel)
        {
            return _students.Where(s => s.YearLevel.Equals(yearLevel, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        #endregion

        #region Data Persistence

        /// <summary>
        /// Save data to JSON file
        /// </summary>
        private void SaveData()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                string jsonString = JsonSerializer.Serialize(_students, options);
                File.WriteAllText(_dataFilePath, jsonString);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to save data: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Load data from JSON file
        /// </summary>
        private void LoadData()
        {
            try
            {
                if (File.Exists(_dataFilePath))
                {
                    string jsonString = File.ReadAllText(_dataFilePath);
                    if (!string.IsNullOrWhiteSpace(jsonString))
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            PropertyNameCaseInsensitive = true
                        };

                        _students = JsonSerializer.Deserialize<List<Student>>(jsonString, options) ?? new List<Student>();

                        // Set the next ID based on existing data
                        _nextId = _students.Count > 0 ? _students.Max(s => s.ID) + 1 : 1;
                    }
                }
                else
                {
                    // Create with sample data for testing
                    CreateSampleData();
                }
            }
            catch (Exception ex)
            {
                // If loading fails, start with empty list
                _students = new List<Student>();
                _nextId = 1;

                // You might want to log this error
                System.Diagnostics.Debug.WriteLine($"Failed to load data: {ex.Message}");
            }
        }

        /// <summary>
        /// Create sample data for testing
        /// </summary>
        private void CreateSampleData()
        {
            var sampleStudents = new List<Student>
            {
                new Student
                {
                    ID = 1,
                    StudentID = "FOE.41.008.140.22",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@university.edu",
                    PhoneNumber = "(555) 123-4567",
                    DateOfBirth = new DateTime(2002, 5, 15),
                    Course = "Computer Science",
                    Faculty = "Engineering",
                    Department = "Computer Science & Engineering",
                    YearLevel = "Third Year",
                    CWA = 3.75,
                    EnrollmentDate = new DateTime(2022, 9, 1)
                },
                new Student
                {
                    ID = 2,
                    StudentID = "FOE.42.009.141.22",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@university.edu",
                    PhoneNumber = "(555) 987-6543",
                    DateOfBirth = new DateTime(2001, 8, 22),
                    Course = "Information Technology",
                    Faculty = "Engineering",
                    Department = "Information Technology",
                    YearLevel = "Fourth Year",
                    CWA = 3.85,
                    EnrollmentDate = new DateTime(2021, 9, 1)
                }
            };

            _students = sampleStudents;
            _nextId = 3;
            SaveData();
        }

        #endregion

        #region Statistics

        /// <summary>
        /// Get total number of students
        /// </summary>
        public int GetTotalStudentCount()
        {
            return _students.Count;
        }

        /// <summary>
        /// Get average CWA
        /// </summary>
        public double GetAverageCWA()
        {
            return _students.Count > 0 ? _students.Average(s => s.CWA) : 0.0;
        }

        /// <summary>
        /// Get students grouped by faculty
        /// </summary>
        public Dictionary<string, int> GetStudentCountByFaculty()
        {
            return _students.GroupBy(s => s.Faculty)
                           .ToDictionary(g => g.Key, g => g.Count());
        }

        #endregion
    }
}
