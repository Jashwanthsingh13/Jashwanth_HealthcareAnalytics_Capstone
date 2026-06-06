using Microsoft.AspNetCore.Mvc;
using HealthcareAnalytics.MVC.Models;

namespace HealthcareAnalytics.MVC.Controllers;

public class AppointmentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(
        AppointmentViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        return RedirectToAction(nameof(Index));
    }
}