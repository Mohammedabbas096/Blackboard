using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Group1Blackboard.Models
{
    public class AppUser
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "First name may contain only letters and spaces.")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a last name.")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Last name may contain only letters and spaces.")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a date of birth.")]
        [PastDate100Years(ErrorMessage = "Date of birth must be a past date, no more than 100 years ago.")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Remote("CheckEmail", "Validation", areaName: "", AdditionalFields = "UserId", ErrorMessage = "Email address is already in use.")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Social Security Number.")]
        [RegularExpression(@"^[\d-]+$", ErrorMessage = "SSN may contain only digits and dashes.")]
        [StringLength(11)]
        public string SSN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a user type.")]
        [StringLength(20)]
        public string UserType { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        // Navigation properties
        public ICollection<Course> TeachingCourses { get; set; } = new List<Course>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
