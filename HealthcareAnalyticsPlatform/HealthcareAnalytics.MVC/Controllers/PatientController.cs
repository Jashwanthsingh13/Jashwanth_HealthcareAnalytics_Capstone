using Microsoft.AspNetCore.Mvc;
using HealthcareAnalytics.MVC.Models;
using System.Net.Http.Json;

namespace HealthcareAnalytics.MVC.Controllers;

public class PatientController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PatientController(
        IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        string? user =
        HttpContext.Session.GetString("User");

        if (user == null)
        {
            return RedirectToAction(
                "Login",
                "Account");
        }

        var client = _httpClientFactory.CreateClient();

        var patients =
            await client.GetFromJsonAsync<List<PatientViewModel>>
            (
                "http://localhost:6001/api/patients"
            );

        return View(patients ?? new List<PatientViewModel>());
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        PatientViewModel model)
    {
        TempData["SuccessMessage"] = "Patient added successfully";

        if (!ModelState.IsValid)
            return View(model);

        var client =
            _httpClientFactory.CreateClient();

        var response =
            await client.PostAsJsonAsync(
                "http://localhost:6001/api/patients",
                model);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(
            "",
            "Unable to save patient.");

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients()
    {
        var client =
            _httpClientFactory.CreateClient();

        var patients =
            await client.GetFromJsonAsync<
                List<PatientViewModel>>
            (
                "http://localhost:6001/api/patients"
            );

        return PartialView(
            "_PatientList",
            patients);
    }
}