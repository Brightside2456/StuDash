using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuDash
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        [Column(TypeName = "varchar(50)")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password hash is required")]
        [StringLength(500)] // Hash will be longer than original password
        [Column(TypeName = "varchar(500)")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        [Column(TypeName = "nvarchar(100)")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required")]
        [StringLength(20, ErrorMessage = "Role cannot exceed 20 characters")]
        [Column(TypeName = "varchar(20)")]
        public string Role { get; set; } = string.Empty; // Admin, Teacher, Registrar, Staff

        [Required]
        public bool IsActive { get; set; } = true;

        [Column(TypeName = "datetime2")]
        public DateTime? LastLogin { get; set; }

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

        // Constructor
        public User()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Username} ({FullName}) - {Role}";
        }
    }

    // Enum for user roles (for better type safety)
    public enum UserRole
    {
        Admin,
        Teacher,
        Registrar,
        Staff
    }
}