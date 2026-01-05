using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace StuDash
{
    public class SchoolDbContext : DbContext
    {
        // DbSet represents a table in the database
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }

        // Constructor - allows dependency injection (for advanced scenarios)
        public SchoolDbContext() : base()
        {
        }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }

        // Configure database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Only configure if not already configured (for flexibility)
            if (!optionsBuilder.IsConfigured)
            {
                // CONNECTION STRING EXPLAINED:
                // Server=.\\SQLEXPRESS  - Your local SQL Server instance
                // Database=SchoolManagementDB - Name of your database
                // Trusted_Connection=True - Use Windows Authentication
                // TrustServerCertificate=True - Required for local development
                // MultipleActiveResultSets=True - Allows multiple queries at once

                optionsBuilder.UseSqlServer(
                    @"Server=.\SQLEXPRESS;Database=SchoolManagementDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True",
                    sqlServerOptions => sqlServerOptions.CommandTimeout(60) // 60 second timeout
                );

                // Enable detailed errors in development
                #if DEBUG
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.EnableDetailedErrors();
                #endif
            }
        }

        // Configure model relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Student entity
            modelBuilder.Entity<Student>(entity =>
            {
                // Set table name explicitly
                entity.ToTable("Students");

                // Configure primary key
                entity.HasKey(s => s.ID);

                // Configure unique constraint on StudentID
                entity.HasIndex(s => s.StudentID)
                      .IsUnique()
                      .HasDatabaseName("IX_Students_StudentID");

                // Configure index on Email for faster searches
                entity.HasIndex(s => s.Email)
                      .HasDatabaseName("IX_Students_Email");

                // Configure default values
                entity.Property(s => s.Status)
                      .HasDefaultValue("Active");

                entity.Property(s => s.CreatedDate)
                      .HasDefaultValueSql("GETDATE()");

                // Ignore computed properties
                entity.Ignore(s => s.FullName);
                entity.Ignore(s => s.Age);
            });

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.UserID);

                // Unique username
                entity.HasIndex(u => u.Username)
                      .IsUnique()
                      .HasDatabaseName("IX_Users_Username");

                // Index on email
                entity.HasIndex(u => u.Email)
                      .HasDatabaseName("IX_Users_Email");

                entity.Property(u => u.IsActive)
                      .HasDefaultValue(true);

                entity.Property(u => u.CreatedDate)
                      .HasDefaultValueSql("GETDATE()");
            });

            // Seed initial data (optional but recommended)
            SeedData(modelBuilder);
        }

        // Add initial data to database
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed default admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = 1,
                    Username = "admin",
                    PasswordHash = HashPassword("admin123"), // Change this in production!
                    FullName = "System Administrator",
                    Email = "admin@school.edu",
                    Role = "Admin",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "System"
                }
            );

            // Seed sample students (optional - remove if not needed)
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    ID = 1,
                    StudentID = "FOE.41.008.140.22",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@school.edu",
                    PhoneNumber = "0241234567",
                    DateOfBirth = new DateTime(2002, 5, 15),
                    Faculty = "Engineering",
                    Department = "Computer Science & Engineering",
                    Course = "Computer Science",
                    YearLevel = "Third Year",
                    CWA = 3.75M,
                    EnrollmentDate = new DateTime(2022, 9, 1),
                    Status = "Active",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "System"
                },
                new Student
                {
                    ID = 2,
                    StudentID = "FOE.42.009.141.22",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@school.edu",
                    PhoneNumber = "0249876543",
                    DateOfBirth = new DateTime(2001, 8, 22),
                    Faculty = "Engineering",
                    Department = "Information Technology",
                    Course = "Information Technology",
                    YearLevel = "Fourth Year",
                    CWA = 3.85M,
                    EnrollmentDate = new DateTime(2021, 9, 1),
                    Status = "Active",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "System"
                }
            );
        }

        // Simple password hashing (for seed data only - improve this later)
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        // Override SaveChanges to automatically update audit fields
        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Student || e.Entity is User)
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is Student student)
                    {
                        student.CreatedDate = DateTime.Now;
                        // student.CreatedBy will be set from SessionManager later
                    }
                    else if (entry.Entity is User user)
                    {
                        user.CreatedDate = DateTime.Now;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is Student student)
                    {
                        student.ModifiedDate = DateTime.Now;
                        // student.ModifiedBy will be set from SessionManager later
                    }
                    else if (entry.Entity is User user)
                    {
                        user.ModifiedDate = DateTime.Now;
                    }
                }
            }
        }
    }
}