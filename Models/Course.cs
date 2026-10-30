using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Group1Blackboard.Models
{
    public class Course
    {
        // Static dropdown choices defined once in model class (Assignment Requirement)
        public static readonly List<string> Schedules = new() { "MWF", "TR", "Online" };

        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please enter a course number (e.g. CS 101).")]
        [StringLength(20)]
        public string CourseNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a course title.")]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Range(1, 6, ErrorMessage = "Units must be between 1 and 6.")]
        public int Units { get; set; } = 3;

        [Required(ErrorMessage = "Please select a schedule.")]
        [StringLength(20)]
        public string Schedule { get; set; } = string.Empty;

        [StringLength(50)]
        public string Time { get; set; } = string.Empty;

        // Foreign key and navigation property for Major
        public int MajorId { get; set; }
        [ValidateNever]
        public Major Major { get; set; } = null!;

        // Foreign key and navigation property for Instructor (AppUser)
        public int InstructorId { get; set; }
        [ValidateNever]
        public AppUser Instructor { get; set; } = null!;

        // Navigation property for Reservations
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
