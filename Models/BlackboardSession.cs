namespace Group1Blackboard.Models
{
    public class BlackboardSession
    {
        private const string PlanKey = "plan_courses";
        private const string CountKey = "plan_count";
        private const string MajorKey = "active_major";

        private ISession Session { get; set; }

        public BlackboardSession(ISession session)
        {
            Session = session;
        }

        public void SetPlanCourseIds(List<int> courseIds)
        {
            Session.SetObject(PlanKey, courseIds);
            Session.SetInt32(CountKey, courseIds.Count);
        }

        public List<int> GetPlanCourseIds()
        {
            return Session.GetObject<List<int>>(PlanKey) ?? new List<int>();
        }

        public int? GetPlanCount()
        {
            return Session.GetInt32(CountKey);
        }

        public void RemovePlan()
        {
            Session.Remove(PlanKey);
            Session.Remove(CountKey);
        }

        public void SetActiveMajor(string activeMajor)
        {
            Session.SetString(MajorKey, activeMajor);
        }

        public string GetActiveMajor()
        {
            return Session.GetString(MajorKey) ?? string.Empty;
        }
    }
}
