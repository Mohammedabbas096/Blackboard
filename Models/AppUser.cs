using System.ComponentModel.DataAnnotations;

namespace Group1Blackboard.Models
{
    public class AppUser
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a last name.")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        // Navigation properties
        public ICollection<Course> TeachingCourses { get; set; } = new List<Course>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
