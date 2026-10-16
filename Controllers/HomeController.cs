using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Group1Blackboard.Models;

namespace Group1Blackboard.Controllers;

public class HomeController : Controller
{
    // Root / redirects to Student dashboard
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Home", new { area = "Student" });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
