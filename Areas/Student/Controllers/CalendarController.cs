using Microsoft.AspNetCore.Mvc;

namespace Group1Blackboard.Areas.Student.Controllers
{
    [Area("Student")]
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
