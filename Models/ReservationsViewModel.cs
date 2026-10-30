namespace Group1Blackboard.Models
{
    public class ReservationsViewModel
    {
        public List<Reservation> Reservations { get; set; } = new();
        public int DemonstrationStudentId { get; set; } = 4;
        public string DemonstrationStudentName { get; set; } = "Alex Mercer";
        public int TotalReservedUnits => Reservations.Sum(r => r.Course?.Units ?? 0);
    }
}
