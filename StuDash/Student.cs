using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuDash
{
    internal class Student
    {
        [Key]
        public int ID { get; set; }

        //[Required(ErrorMessage = "Student ID is required")]
        //[StringLength(20, ErrorMessage = "Student ID cannot exceed 20 characters")]
        public string StudentID { get; set; }

        //[Required(ErrorMessage = "First name is required")]
        //[StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Last name is required")]
        //[StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Email is required")]
        //[EmailAddress(ErrorMessage = "Please enter a valid email address")]
        //[StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        //[Phone(ErrorMessage = "Please enter a valid phone number")]
        //[StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string PhoneNumber { get; set; }

        //[Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        //[Required(ErrorMessage = "Course is required")]
        //[StringLength(100, ErrorMessage = "Course cannot exceed 100 characters")]
        public string Course { get; set; }

        //[Required(ErrorMessage = "Faculty is required")]
        //[StringLength(100, ErrorMessage = "Faculty cannot exceed 100 characters")]
        public string Faculty { get; set; }

        //[Required(ErrorMessage = "Department is required")]
        //[StringLength(100, ErrorMessage = "Department cannot exceed 100 characters")]
        public string Department { get; set; }

        //[Required(ErrorMessage = "Year level is required")]
        public string YearLevel { get; set; }

        //[Range(0.0, 4.0, ErrorMessage = "CWA must be between 0.0 and 4.0")]
        public double CWA { get; set; }

        //[Required(ErrorMessage = "Enrollment date is required")]
        public DateTime EnrollmentDate { get; set; }

        // Computed properties for display
        public string FullName => $"{FirstName} {LastName}";

        public int Age => DateTime.Now.Year - DateOfBirth.Year -
                         (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);

        // Constructor
        public Student()
        {
            // Set default values
            DateOfBirth = DateTime.Now.AddYears(-18); // Default to 18 years old
            EnrollmentDate = DateTime.Now;
            CWA = 0.0;
        }

        // Override ToString for better display in lists
        public override string ToString()
        {
            return $"{StudentID} - {FullName}";
        }

        // Method to validate the student object
        //public bool IsValid()
        //{
        //    return !string.IsNullOrWhiteSpace(StudentID) &&
        //           !string.IsNullOrWhiteSpace(FirstName) &&
        //           !string.IsNullOrWhiteSpace(LastName) &&
        //           !string.IsNullOrWhiteSpace(Email) &&
        //           !string.IsNullOrWhiteSpace(Course) &&
        //           !string.IsNullOrWhiteSpace(Faculty) &&
        //           !string.IsNullOrWhiteSpace(Department) &&
        //           !string.IsNullOrWhiteSpace(YearLevel) &&
        //           DateOfBirth < DateTime.Now &&
        //           EnrollmentDate <= DateTime.Now &&
        //           CWA >= 0.0 && CWA <= 4.0;
        //}
    }
}
