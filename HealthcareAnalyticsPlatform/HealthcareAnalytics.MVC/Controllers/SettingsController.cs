using Microsoft.AspNetCore.Mvc;
using HealthcareAnalytics.MVC.Models;

namespace HealthcareAnalytics.MVC.Controllers;

public class SettingsController : Controller
{
    public IActionResult Index()
    {
        if (HttpContext.Session
            .GetString("User") == null)
        {
            return RedirectToAction(
                "Login",
                "Account");
        }

        SettingsViewModel model = new()
        {
            Username = "admin",
            Email = "admin@healthcare.com",
            Role = "Administrator",
            TotalUsers = 3,
            TotalDoctors = 4,
            TotalPatients = 5
        };

        return View(model);
    }
}