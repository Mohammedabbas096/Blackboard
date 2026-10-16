using Microsoft.AspNetCore.Mvc;

namespace Group1Blackboard.Areas.Student.Controllers
{
    [Area("Student")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
