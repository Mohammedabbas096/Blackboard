using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Group1Blackboard.Models
{
    public class Course
    {
        // Static dropdown choices defined once in model class (Assignment Requirement)
        public static readonly List<string> Schedules = new() { "MWF", "TR", "Online" };

        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please enter a course number.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Course number may contain only letters and digits.")]
        [Remote("CheckCourseNumber", "Validation", areaName: "", AdditionalFields = "CourseId", ErrorMessage = "Course number is already in use.")]
        [StringLength(20)]
        public string CourseNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a course title.")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Course name may contain only letters, numbers, and spaces.")]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a description.")]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter credits.")]
        [Range(1, 6, ErrorMessage = "Units must be between 1 and 6.")]
        public int Units { get; set; } = 3;

        [Required(ErrorMessage = "Please select a schedule.")]
        [StringLength(20)]
        public string Schedule { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter class times.")]
        [StringLength(50)]
        public string Time { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a semester.")]
        [StringLength(50)]
        public string Semester { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a location.")]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty;

        // Foreign key and navigation property for Major
        [Required(ErrorMessage = "Please select a major.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a major.")]
        public int MajorId { get; set; }
        [ValidateNever]
        public Major Major { get; set; } = null!;

        // Foreign key and navigation property for Instructor (AppUser)
        [Required(ErrorMessage = "Please select an instructor.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select an instructor.")]
        public int InstructorId { get; set; }
        [ValidateNever]
        public AppUser Instructor { get; set; } = null!;

        // Navigation property for Reservations
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
