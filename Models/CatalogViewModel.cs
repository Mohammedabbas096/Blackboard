namespace Group1Blackboard.Models
{
    public class CatalogViewModel
    {
        public List<Course> Courses { get; set; } = new();
        public List<Major> Majors { get; set; } = new();
        public int SelectedMajorId { get; set; } = 0; // 0 = All Majors
        public List<int> PlannedCourseIds { get; set; } = new();
        public List<int> ReservedCourseIds { get; set; } = new();
    }
}
