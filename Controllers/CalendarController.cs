using Microsoft.AspNetCore.Mvc;

namespace Group1Blackboard.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return Content("Area: (root) | Controller: Calendar | Action: Index");
        }
    }
}
