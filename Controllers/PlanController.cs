using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Group1Blackboard.Models;

namespace Group1Blackboard.Controllers
{
    public class PlanController : Controller
    {
        private const int DemonstrationStudentId = 4;
        private readonly BlackboardContext _context;

        public PlanController(BlackboardContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var session = new BlackboardSession(HttpContext.Session);

            // Restore from cookie if session is empty
            RestorePlanFromCookieIfEmpty(session);

            var planIds = session.GetPlanCourseIds();

            var courses = _context.Courses
                .Include(c => c.Major)
                .Include(c => c.Instructor)
                .Where(c => planIds.Contains(c.CourseId))
                .ToList();

            var existingReservedIds = _context.Reservations
                .Where(r => r.StudentId == DemonstrationStudentId && planIds.Contains(r.CourseId))
                .Select(r => r.CourseId)
                .ToList();

            var viewModel = new PlanViewModel
            {
                PlannedCourses = courses,
                DemonstrationStudentId = DemonstrationStudentId,
                DemonstrationStudentName = "Alex Mercer",
                ExistingReservationCourseIds = existingReservedIds
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(int courseId, int? returnMajorId)
        {
            var course = _context.Courses.Find(courseId);
            if (course == null)
            {
                TempData["error"] = "Selected course was not found.";
                return RedirectToAction("Index", "Catalog", new { majorId = returnMajorId ?? 0 });
            }

            var session = new BlackboardSession(HttpContext.Session);
            var cookies = new BlackboardCookies(Response.Cookies);

            var planIds = session.GetPlanCourseIds();
            if (planIds.Contains(courseId))
            {
                TempData["error"] = $"{course.CourseNumber} is already in your course plan.";
            }
            else
            {
                planIds.Add(courseId);
                session.SetPlanCourseIds(planIds);
                cookies.SetPlanCourseIds(planIds);
                TempData["message"] = $"{course.CourseNumber} ({course.Title}) was added to your course plan.";
            }

            // Post-Redirect-Get: redirect back to catalog with preserved filter
            return RedirectToAction("Index", "Catalog", new { majorId = returnMajorId ?? 0 });
        }

        [HttpPost]
        public IActionResult Remove(int courseId)
        {
            var session = new BlackboardSession(HttpContext.Session);
            var cookies = new BlackboardCookies(Response.Cookies);

            var planIds = session.GetPlanCourseIds();
            if (planIds.Remove(courseId))
            {
                session.SetPlanCourseIds(planIds);
                cookies.SetPlanCourseIds(planIds);
                TempData["message"] = "Course removed from your plan.";
            }

            // Post-Redirect-Get
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Clear()
        {
            var session = new BlackboardSession(HttpContext.Session);
            var cookies = new BlackboardCookies(Response.Cookies);

            session.RemovePlan();
            cookies.RemovePlanCookie();

            TempData["message"] = "Your course plan has been cleared.";

            // Post-Redirect-Get
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reserve()
        {
            var session = new BlackboardSession(HttpContext.Session);
            var cookies = new BlackboardCookies(Response.Cookies);

            var planIds = session.GetPlanCourseIds();
            if (planIds.Count == 0)
            {
                TempData["error"] = "Your plan is empty. Please add courses to your plan before reserving.";
                return RedirectToAction("Index", "Catalog");
            }

            var student = _context.Users.Find(DemonstrationStudentId);
            if (student == null)
            {
                TempData["error"] = "Demonstration student profile was not found.";
                return RedirectToAction("Index");
            }

            // Check existing reservations for this student to prevent duplicates
            var existingReservedIds = _context.Reservations
                .Where(r => r.StudentId == DemonstrationStudentId && planIds.Contains(r.CourseId))
                .Select(r => r.CourseId)
                .ToList();

            var newCourseIds = planIds.Except(existingReservedIds).ToList();

            if (newCourseIds.Count == 0)
            {
                TempData["error"] = "All courses in your plan have already been reserved.";
                return RedirectToAction("Reservations");
            }

            // Create persistent reservation records in SQLite
            foreach (var courseId in newCourseIds)
            {
                var reservation = new Reservation
                {
                    CourseId = courseId,
                    StudentId = DemonstrationStudentId,
                    ReservationDate = DateTime.UtcNow,
                    Status = "Confirmed"
                };
                _context.Reservations.Add(reservation);
            }

            _context.SaveChanges();

            // Clear the ephemeral plan from session and persistent cookie ONLY after DB save succeeds
            session.RemovePlan();
            cookies.RemovePlanCookie();

            TempData["message"] = $"Successfully reserved {newCourseIds.Count} course(s)!";

            // Post-Redirect-Get: redirect to confirmed reservations list
            return RedirectToAction("Reservations");
        }

        [HttpGet]
        public IActionResult Reservations()
        {
            var reservations = _context.Reservations
                .Where(r => r.StudentId == DemonstrationStudentId)
                .Include(r => r.Course)
                    .ThenInclude(c => c.Major)
                .Include(r => r.Course.Instructor)
                .OrderByDescending(r => r.ReservationDate)
                .ToList();

            var viewModel = new ReservationsViewModel
            {
                Reservations = reservations,
                DemonstrationStudentId = DemonstrationStudentId,
                DemonstrationStudentName = "Alex Mercer"
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
                    var validIds = _context.Courses
                        .Where(c => cookieIds.Contains(c.CourseId))
                        .Select(c => c.CourseId)
                        .ToList();

                    session.SetPlanCourseIds(validIds);

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
