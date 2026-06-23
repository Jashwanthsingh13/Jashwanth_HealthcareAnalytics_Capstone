using Microsoft.AspNetCore.Mvc;
using HealthcareAnalytics.MVC.Models;

namespace HealthcareAnalytics.MVC.Controllers;

public class AppointmentController : Controller
{
    private static List<AppointmentViewModel> appointments =
        new();

    public IActionResult Index()
    {
        string? user =
            HttpContext.Session.GetString("User");

        if (user == null)
        {
            return RedirectToAction(
                "Login",
                "Account");
        }

        return View(appointments);
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

        model.AppointmentId =
            appointments.Count + 1;

        // Demo mapping
        model.PatientName =
            model.PatientId switch
            {
                1 => "John Smith",
                2 => "Jane Doe",
                _ => "Unknown Patient"
            };

        model.DoctorName =
            model.DoctorId switch
            {
                1 => "Dr Emily",
                2 => "Dr Michael",
                _ => "Unknown Doctor"
            };

        appointments.Add(model);

        TempData["Success"] =
            "Appointment scheduled successfully";

        return RedirectToAction(nameof(Index));
    }
}