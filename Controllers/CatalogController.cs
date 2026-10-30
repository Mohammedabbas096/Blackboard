using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Group1Blackboard.Models;

namespace Group1Blackboard.Controllers
{
    public class CatalogController : Controller
    {
        private const int DemonstrationStudentId = 4;
        private readonly BlackboardContext _context;

        public CatalogController(BlackboardContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int? majorId)
        {
            var session = new BlackboardSession(HttpContext.Session);

            // Restore course plan from persistent cookie if session is empty
            RestorePlanFromCookieIfEmpty(session);

            // Determine active major filter (via route/query or session memory)
            int activeMajorId = 0;
            if (majorId.HasValue)
            {
                activeMajorId = majorId.Value;
                session.SetActiveMajor(activeMajorId.ToString());
            }
            else
            {
                string savedMajor = session.GetActiveMajor();
                if (int.TryParse(savedMajor, out int parsedId))
                {
                    activeMajorId = parsedId;
                }
            }

            var majors = _context.Majors.OrderBy(m => m.Name).ToList();

            IQueryable<Course> query = _context.Courses
                .Include(c => c.Major)
                .Include(c => c.Instructor)
                .OrderBy(c => c.CourseNumber);

            if (activeMajorId > 0)
            {
                query = query.Where(c => c.MajorId == activeMajorId);
            }

            var courses = query.ToList();
            var plannedIds = session.GetPlanCourseIds();

            // Check existing reservations for the demonstration student (Alex Mercer, ID 4)
            var reservedIds = _context.Reservations
                .Where(r => r.StudentId == DemonstrationStudentId)
                .Select(r => r.CourseId)
                .ToList();

            var viewModel = new CatalogViewModel
            {
                Courses = courses,
                Majors = majors,
                SelectedMajorId = activeMajorId,
                PlannedCourseIds = plannedIds,
                ReservedCourseIds = reservedIds
            };

            return View(viewModel);
        }

        private void RestorePlanFromCookieIfEmpty(BlackboardSession session)
        {
            if (!session.GetPlanCount().HasValue)
            {
                var cookies = new BlackboardCookies(Request.Cookies);
                var cookieIds = cookies.GetPlanCourseIds();

                if (cookieIds.Count > 0)
                {
                    // Validate IDs against SQLite database to prevent ghost/stale entries
                    var validIds = _context.Courses
                        .Where(c => cookieIds.Contains(c.CourseId))
                        .Select(c => c.CourseId)
                        .ToList();

                    session.SetPlanCourseIds(validIds);

                    // If cookie contained invalid/deleted IDs, resynchronize cookie
                    if (validIds.Count != cookieIds.Count)
                    {
                        var responseCookies = new BlackboardCookies(Response.Cookies);
                        responseCookies.SetPlanCourseIds(validIds);
                    }
                }
            }
        }
    }
}
