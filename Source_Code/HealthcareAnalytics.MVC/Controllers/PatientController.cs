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

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        PatientViewModel model)
    {
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