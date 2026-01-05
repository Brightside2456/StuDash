using Microsoft.EntityFrameworkCore;
using StuDash;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StuDash
{
    public class StudentService : IDisposable
    {
        private readonly SchoolDbContext _context;

        // Constructor - creates database connection
        public StudentService()
        {
            _context = new SchoolDbContext();
            
            // Ensure database exists
            try
            {
                _context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to connect to database. " +
                    "Please ensure SQL Server is running.", ex);
            }
        }

        #region CRUD Operations

        /// <summary>
        /// Get all students from database
        /// </summary>
        public List<Student> GetAllStudents()
        {
            try
            {
                // ToList() executes the query and returns results
                return _context.Students
                    .OrderBy(s => s.StudentID)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to retrieve students: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get student by internal ID
        /// </summary>
        public Student? GetStudentById(int id)
        {
            try
            {
                // Find() is optimized for primary key lookups
                return _context.Students.Find(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to retrieve student: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get student by Student ID (e.g., FOE.41.008.140.22)
        /// </summary>
        public Student? GetStudentByStudentId(string studentId)
        {
            try
            {
                return _context.Students
                    .FirstOrDefault(s => s.StudentID == studentId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to retrieve student: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Add a new student to database
        /// </summary>
        public bool AddStudent(Student student)
        {
            try
            {
                // Validation: Check for duplicate Student ID
                if (GetStudentByStudentId(student.StudentID) != null)
                {
                    throw new ArgumentException($"Student ID '{student.StudentID}' already exists.");
                }

                // Validation: Ensure required fields
                if (!student.IsValid())
                {
                    throw new ArgumentException("Student data is incomplete or invalid.");
                }

                // Set audit fields
                student.CreatedDate = DateTime.Now;
                // TODO: Set CreatedBy from current logged-in user

                // Add to context (tracked by EF Core)
                _context.Students.Add(student);

                // Save to database
                int rowsAffected = _context.SaveChanges();

                return rowsAffected > 0;
            }
            catch (DbUpdateException dbEx)
            {
                // Database-specific errors (constraint violations, etc.)
                throw new ApplicationException($"Database error adding student: {dbEx.InnerException?.Message ?? dbEx.Message}", dbEx);
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
                // Find existing student in database
                var existingStudent = GetStudentById(updatedStudent.ID);
                if (existingStudent == null)
                {
                    throw new ArgumentException($"Student with ID {updatedStudent.ID} not found.");
                }

                // Check for StudentID conflicts with other students
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
                existingStudent.Faculty = updatedStudent.Faculty;
                existingStudent.Department = updatedStudent.Department;
                existingStudent.Course = updatedStudent.Course;
                existingStudent.YearLevel = updatedStudent.YearLevel;
                existingStudent.CWA = updatedStudent.CWA;
                existingStudent.EnrollmentDate = updatedStudent.EnrollmentDate;
                existingStudent.Status = updatedStudent.Status;
                
                // Audit fields are automatically updated by DbContext.SaveChanges()
                existingStudent.ModifiedDate = DateTime.Now;
                // TODO: Set ModifiedBy from current logged-in user

                // Mark as modified (EF Core tracks this automatically, but being explicit)
                _context.Entry(existingStudent).State = EntityState.Modified;

                // Save changes
                int rowsAffected = _context.SaveChanges();

                return rowsAffected > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Another user modified this record
                throw new ApplicationException("This student record was modified by another user. Please refresh and try again.");
            }
            catch (DbUpdateException dbEx)
            {
                throw new ApplicationException($"Database error updating student: {dbEx.InnerException?.Message ?? dbEx.Message}", dbEx);
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

                // Option 1: Hard delete (permanently remove)
                _context.Students.Remove(student);

                // Option 2: Soft delete (mark as inactive - better for audit trail)
                // student.Status = "Deleted";
                // student.ModifiedDate = DateTime.Now;
                // _context.Entry(student).State = EntityState.Modified;

                int rowsAffected = _context.SaveChanges();

                return rowsAffected > 0;
            }
            catch (DbUpdateException dbEx)
            {
                // Handle foreign key constraints (when we add related tables)
                throw new ApplicationException($"Cannot delete student: {dbEx.InnerException?.Message ?? dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to delete student: {ex.Message}", ex);
            }
        }

        #endregion

        #region Search and Filter

        /// <summary>
        /// Search students by multiple criteria
        /// </summary>
        public List<Student> SearchStudents(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return GetAllStudents();

                searchTerm = searchTerm.ToLower();

                // Using LINQ to build query
                // EF Core translates this to SQL automatically
                return _context.Students
                    .Where(s =>
                        s.StudentID.ToLower().Contains(searchTerm) ||
                        s.FirstName.ToLower().Contains(searchTerm) ||
                        s.LastName.ToLower().Contains(searchTerm) ||
                        s.Email.ToLower().Contains(searchTerm) ||
                        s.Course.ToLower().Contains(searchTerm) ||
                        s.Faculty.ToLower().Contains(searchTerm) ||
                        s.Department.ToLower().Contains(searchTerm)
                    )
                    .OrderBy(s => s.StudentID)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Search failed: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get students by faculty
        /// </summary>
        public List<Student> GetStudentsByFaculty(string? faculty)
        {
            try
            {
                return _context.Students
                    .Where(s => s.Faculty == faculty)
                    .OrderBy(s => s.StudentID)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to retrieve students by faculty: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get students by year level
        /// </summary>
        public List<Student> GetStudentsByYearLevel(string? yearLevel)
        {
            try
            {
                return _context.Students
                    .Where(s => s.YearLevel == yearLevel)
                    .OrderBy(s => s.StudentID)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to retrieve students by year level: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get students by status
        /// </summary>
        public List<Student> GetStudentsByStatus(string? status)
        {
            try
            {
                return _context.Students
                    .Where(s => s.Status == status)
                    .OrderBy(s => s.StudentID)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to retrieve students by status: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Advanced search with multiple filters
        /// </summary>
        public List<Student> AdvancedSearch(string? searchTerm = null, string? faculty = null, 
            string? yearLevel = null, string? status = "Active")
        {
            try
            {
                // Start with all students
                var query = _context.Students.AsQueryable();

                // Apply filters conditionally
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    query = query.Where(s =>
                        s.StudentID.ToLower().Contains(searchTerm) ||
                        s.FirstName.ToLower().Contains(searchTerm) ||
                        s.LastName.ToLower().Contains(searchTerm) ||
                        s.Email.ToLower().Contains(searchTerm)
                    );
                }

                if (!string.IsNullOrWhiteSpace(faculty))
                {
                    query = query.Where(s => s.Faculty == faculty);
                }

                if (!string.IsNullOrWhiteSpace(yearLevel))
                {
                    query = query.Where(s => s.YearLevel == yearLevel);
                }

                if (!string.IsNullOrWhiteSpace(status))
                {
                    query = query.Where(s => s.Status == status);
                }

                return query.OrderBy(s => s.StudentID).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Advanced search failed: {ex.Message}", ex);
            }
        }

        #endregion

        #region Statistics

        /// <summary>
        /// Get total student count
        /// </summary>
        public int GetTotalStudentCount()
        {
            try
            {
                return _context.Students.Count();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to count students: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get active student count
        /// </summary>
        public int GetActiveStudentCount()
        {
            try
            {
                return _context.Students.Count(s => s.Status == "Active");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to count active students: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get average CWA
        /// </summary>
        public decimal GetAverageCWA()
        {
            try
            {
                var count = _context.Students.Count();
                if (count == 0) return 0;

                return _context.Students.Average(s => s.CWA);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to calculate average CWA: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get students grouped by faculty
        /// </summary>
        public Dictionary<string, int> GetStudentCountByFaculty()
        {
            try
            {
                return _context.Students
                    .GroupBy(s => s.Faculty)
                    .ToDictionary(g => g.Key, g => g.Count());
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to group students by faculty: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get students grouped by year level
        /// </summary>
        public Dictionary<string, int> GetStudentCountByYearLevel()
        {
            try
            {
                return _context.Students
                    .GroupBy(s => s.YearLevel)
                    .ToDictionary(g => g.Key, g => g.Count());
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to group students by year level: {ex.Message}", ex);
            }
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Dispose of database context (important for preventing memory leaks)
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
        }

        #endregion
    }
}