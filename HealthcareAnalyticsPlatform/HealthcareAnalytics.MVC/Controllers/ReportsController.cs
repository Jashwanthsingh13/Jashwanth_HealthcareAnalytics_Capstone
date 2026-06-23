using Microsoft.AspNetCore.Mvc;

namespace HealthcareAnalytics.MVC.Controllers;

public class ReportsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}