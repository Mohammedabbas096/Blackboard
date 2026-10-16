using Microsoft.AspNetCore.Mvc;

namespace Group1Blackboard.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MajorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
