using Microsoft.AspNetCore.Mvc;
using Group1Blackboard.Models;

namespace Group1Blackboard.Controllers
{
    public class ValidationController : Controller
    {
        private readonly BlackboardContext _context;

        public ValidationController(BlackboardContext context)
        {
            _context = context;
        }

        public IActionResult CheckEmail(string email, int userId = 0)
        {
            string msg = Check.EmailExists(_context, email, userId);
            if (string.IsNullOrEmpty(msg))
            {
                return Json(true);
            }
            return Json(msg);
        }

        public IActionResult CheckCourseNumber(string courseNumber, int courseId = 0)
        {
            string msg = Check.CourseNumberExists(_context, courseNumber, courseId);
            if (string.IsNullOrEmpty(msg))
            {
                return Json(true);
            }
            return Json(msg);
        }
    }
}

