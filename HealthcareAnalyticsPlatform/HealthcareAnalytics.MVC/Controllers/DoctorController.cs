using Microsoft.AspNetCore.Mvc;
using HealthcareAnalytics.MVC.Models;

namespace HealthcareAnalytics.MVC.Controllers;

public class DoctorController : Controller
{
    private static List<DoctorViewModel> doctors =
    [
        new()
        {
            DoctorId = 1,
            DoctorName = "Dr Emily",
            Specialization = "Cardiology",
            Email = "emily@hospital.com",
            PhoneNumber = "9999991111"
        }
    ];

    public IActionResult Index()
    {
        return View(doctors);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(
        DoctorViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        model.DoctorId =
            doctors.Count + 1;

        doctors.Add(model);

        TempData["Success"] =
            "Doctor added successfully!";

        return RedirectToAction(nameof(Index));
    }
}