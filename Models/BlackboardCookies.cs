namespace Group1Blackboard.Models
{
    public class BlackboardCookies
    {
        private const string PlanCookieName = "blackboard_plan";
        private const string Delimiter = "-";

        private IRequestCookieCollection requestCookies { get; set; } = null!;
        private IResponseCookies responseCookies { get; set; } = null!;

        // Constructor 1: Used when reading cookies from HttpRequest
        public BlackboardCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }

        // Constructor 2: Used when writing or deleting cookies via HttpResponse
        public BlackboardCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }

        public void SetPlanCourseIds(List<int> courseIds)
        {
            var validIds = courseIds.Where(id => id > 0).Distinct().ToList();
            string idsString = string.Join(Delimiter, validIds);

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(14),
                HttpOnly = true,
                IsEssential = true
            };

            RemovePlanCookie(); // Delete old cookie before setting updated one
            responseCookies.Append(PlanCookieName, idsString, options);
        }

        public List<int> GetPlanCourseIds()
        {
            string cookieValue = requestCookies?[PlanCookieName] ?? string.Empty;
            if (string.IsNullOrWhiteSpace(cookieValue))
            {
                return new List<int>();
            }

            var ids = new List<int>();
            string[] tokens = cookieValue.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                if (int.TryParse(token.Trim(), out int id) && id > 0)
                {
                    ids.Add(id);
                }
            }

            return ids;
        }

        public void RemovePlanCookie()
        {
            responseCookies?.Delete(PlanCookieName);
        }
    }
}
