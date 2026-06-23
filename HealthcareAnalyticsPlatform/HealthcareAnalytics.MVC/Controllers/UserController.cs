using Microsoft.AspNetCore.Mvc;

namespace HealthcareAnalytics.MVC.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}