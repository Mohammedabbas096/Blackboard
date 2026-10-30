using System.ComponentModel.DataAnnotations;

namespace Group1Blackboard.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        // Foreign key and navigation property for Course
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        // Foreign key and navigation property for Student (AppUser)
        public int StudentId { get; set; }
        public AppUser Student { get; set; } = null!;

        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;

        [StringLength(30)]
        public string Status { get; set; } = "Confirmed";
    }
}
