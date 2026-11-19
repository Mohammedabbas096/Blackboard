using Microsoft.EntityFrameworkCore;

namespace Group1Blackboard.Models
{
    public class BlackboardContext : DbContext
    {
        public BlackboardContext(DbContextOptions<BlackboardContext> options)
            : base(options)
        { }

        public DbSet<Major> Majors { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<AppUser> Users { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map AppUser entity to Users table and configure PK
            modelBuilder.Entity<AppUser>().ToTable("Users");
            modelBuilder.Entity<AppUser>().HasKey(u => u.UserId);

            // Course to Major (One-to-Many)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Major)
                .WithMany(m => m.Courses)
                .HasForeignKey(c => c.MajorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course to Instructor (AppUser) (One-to-Many)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(u => u.TeachingCourses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reservation to Course (One-to-Many)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reservation to Student (AppUser) (One-to-Many)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Student)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prevent duplicate reservations for the same student and course
            modelBuilder.Entity<Reservation>()
                .HasIndex(r => new { r.StudentId, r.CourseId })
                .IsUnique();

            // Seed sample data
            modelBuilder.Entity<Major>().HasData(
                new Major { MajorId = 1, Name = "Computer Science" },
                new Major { MajorId = 2, Name = "Information Technology" },
                new Major { MajorId = 3, Name = "Mathematics" },
                new Major { MajorId = 4, Name = "English" }
            );

            modelBuilder.Entity<AppUser>().HasData(
                // Seeded Instructors
                new AppUser
                {
                    UserId = 1,
                    FirstName = "Alan",
                    LastName = "Turing",
                    Email = "alan.turing@university.edu",
                    DOB = new DateTime(1954, 6, 23),
                    SSN = "111-11-1111",
                    UserType = "Instructor"
                },
                new AppUser
                {
                    UserId = 2,
                    FirstName = "Grace",
                    LastName = "Hopper",
                    Email = "grace.hopper@university.edu",
                    DOB = new DateTime(1960, 12, 9),
                    SSN = "222-22-2222",
                    UserType = "Instructor"
                },
                new AppUser
                {
                    UserId = 3,
                    FirstName = "Katherine",
                    LastName = "Johnson",
                    Email = "katherine.j@university.edu",
                    DOB = new DateTime(1968, 8, 26),
                    SSN = "333-33-3333",
                    UserType = "Instructor"
                },
                // Seeded Demonstration Student
                new AppUser
                {
                    UserId = 4,
                    FirstName = "Alex",
                    LastName = "Mercer",
                    Email = "alex.mercer@student.edu",
                    DOB = new DateTime(2002, 5, 14),
                    SSN = "444-44-4444",
                    UserType = "Student"
                }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    CourseId = 1,
                    CourseNumber = "CS101",
                    Title = "Introduction to Computer Science",
                    Description = "Fundamental programming principles and problem solving using C#.",
                    Units = 3,
                    Schedule = "MWF",
                    Time = "09:00 AM - 09:50 AM",
                    Semester = "Fall 2026",
                    Location = "Hall 101",
                    MajorId = 1,
                    InstructorId = 1
                },
                new Course
                {
                    CourseId = 2,
                    CourseNumber = "CS201",
                    Title = "Data Structures and Algorithms",
                    Description = "Study of fundamental data structures including lists, stacks, queues, trees, and graphs.",
                    Units = 4,
                    Schedule = "TR",
                    Time = "10:00 AM - 11:15 AM",
                    Semester = "Fall 2026",
                    Location = "Lab 204",
                    MajorId = 1,
                    InstructorId = 1
                },
                new Course
                {
                    CourseId = 3,
                    CourseNumber = "IT210",
                    Title = "Web Development with ASP.NET Core",
                    Description = "Building modern web applications using MVC, Razor, and Entity Framework Core.",
                    Units = 3,
                    Schedule = "MWF",
                    Time = "11:00 AM - 11:50 AM",
                    Semester = "Fall 2026",
                    Location = "Technology Center 105",
                    MajorId = 2,
                    InstructorId = 2
                },
                new Course
                {
                    CourseId = 4,
                    CourseNumber = "IT330",
                    Title = "Database Systems & Design",
                    Description = "Relational database concepts, SQL queries, normalization, and database design.",
                    Units = 3,
                    Schedule = "TR",
                    Time = "01:00 PM - 02:15 PM",
                    Semester = "Spring 2027",
                    Location = "Technology Center 210",
                    MajorId = 2,
                    InstructorId = 2
                },
                new Course
                {
                    CourseId = 5,
                    CourseNumber = "MATH150",
                    Title = "Calculus I",
                    Description = "Limits, continuity, derivatives, and introduction to integration.",
                    Units = 4,
                    Schedule = "MWF",
                    Time = "08:00 AM - 08:50 AM",
                    Semester = "Fall 2026",
                    Location = "Science Hall 301",
                    MajorId = 3,
                    InstructorId = 3
                },
                new Course
                {
                    CourseId = 6,
                    CourseNumber = "ENG101",
                    Title = "Academic Writing",
                    Description = "College-level expository and analytical writing with rhetorical focus.",
                    Units = 3,
                    Schedule = "Online",
                    Time = "Asynchronous",
                    Semester = "Fall 2026",
                    Location = "Online",
                    MajorId = 4,
                    InstructorId = 3
                }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    ReservationId = 1,
                    CourseId = 1,
                    StudentId = 4,
                    ReservationDate = new DateTime(2026, 1, 15, 10, 0, 0, DateTimeKind.Utc),
                    Status = "Confirmed"
                }
            );
        }
    }
}
