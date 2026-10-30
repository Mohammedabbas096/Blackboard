namespace Group1Blackboard.Models
{
    public class PlanViewModel
    {
        public List<Course> PlannedCourses { get; set; } = new();
        public int TotalUnits => PlannedCourses.Sum(c => c.Units);
        public int DemonstrationStudentId { get; set; } = 4;
        public string DemonstrationStudentName { get; set; } = "Alex Mercer";
        public List<int> ExistingReservationCourseIds { get; set; } = new();
    }
}
