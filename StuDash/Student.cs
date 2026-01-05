using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuDash
{
    [Table("Students")] // Explicitly name the table
    public class Student
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int ID { get; set; }

        // Student Identification
        [Required(ErrorMessage = "Student ID is required")]
        [StringLength(20, ErrorMessage = "Student ID cannot exceed 20 characters")]
        [Column("StudentID", TypeName = "varchar(20)")]
        public string StudentID { get; set; } = string.Empty;

        // Personal Information
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        [Column(TypeName = "varchar(20)")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required")]
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        // Academic Information
        [Required(ErrorMessage = "Faculty is required")]
        [StringLength(100, ErrorMessage = "Faculty cannot exceed 100 characters")]
        [Column(TypeName = "nvarchar(100)")]
        public string Faculty { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required")]
        [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters")]
        [Column(TypeName = "nvarchar(100)")]
        public string Department { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course is required")]
        [StringLength(100, ErrorMessage = "Course cannot exceed 100 characters")]
        [Column(TypeName = "nvarchar(100)")]
        public string Course { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year level is required")]
        [StringLength(20, ErrorMessage = "Year level cannot exceed 20 characters")]
        [Column(TypeName = "varchar(20)")]
        public string YearLevel { get; set; } = string.Empty;

        [Range(0.0, 4.0, ErrorMessage = "CWA must be between 0.0 and 4.0")]
        [Column(TypeName = "decimal(3,2)")] // e.g., 3.75
        public decimal CWA { get; set; } = 0.00M; // Changed from double to decimal for precision

        [Required(ErrorMessage = "Enrollment date is required")]
        [Column(TypeName = "date")]
        public DateTime EnrollmentDate { get; set; }

        // Student Status
        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Status { get; set; } = "Active"; // Active, Graduated, Suspended, Withdrawn

        // Audit Fields (Track who created/modified and when)
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string CreatedBy { get; set; } = string.Empty;

        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string ModifiedBy { get; set; } = string.Empty;

        // Computed Properties (NOT stored in database)
        [NotMapped] // Tell EF Core not to create a column for this
        public string FullName => $"{FirstName} {LastName}";

        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        // Navigation Properties (for relationships - we'll use these later)
        // public virtual ICollection<Enrollment> Enrollments { get; set; }

        // Constructor
        public Student()
        {
            // Properties already initialized with default values above
        }

        // Method to validate the student object
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(StudentID) &&
                   !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Course) &&
                   !string.IsNullOrWhiteSpace(Faculty) &&
                   !string.IsNullOrWhiteSpace(Department) &&
                   !string.IsNullOrWhiteSpace(YearLevel) &&
                   DateOfBirth < DateTime.Now &&
                   EnrollmentDate <= DateTime.Now &&
                   CWA >= 0.0M && CWA <= 4.0M;
        }

        public override string ToString()
        {
            return $"{StudentID} - {FullName}";
        }
    }
}