using System.ComponentModel.DataAnnotations;

namespace Group1Blackboard.Models
{
    public class Major
    {
        public int MajorId { get; set; }

        [Required(ErrorMessage = "Please enter a major name.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
