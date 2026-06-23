using Microsoft.AspNetCore.Mvc;
using HealthcareAnalytics.MVC.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;


namespace HealthcareAnalytics.MVC.Controllers;

public class DashboardController : Controller
{
    private readonly HttpClient _httpClient;

public DashboardController(
    IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
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

        DashboardViewModel model = new();

        try
        {
            var dashboard =
                await _httpClient.GetFromJsonAsync<DashboardViewModel>(
                    "http://localhost:6001/api/dashboard");

            if (dashboard != null)
            {
                model = dashboard;
            }
        }
        catch (Exception)
        {
            model.TotalPatients = 0;
            model.TotalAppointments = 0;
            model.PendingReviews = 0;
            model.Satisfaction = 0;
        }

        model.Activities = new List<ActivityViewModel>
{
    new ActivityViewModel
    {
        ActivityDescription = "Patient Added",
        ActivityDate = DateTime.Now
    },

    new ActivityViewModel
    {
        ActivityDescription = "Appointment Scheduled",
        ActivityDate = DateTime.Now
    },

    new ActivityViewModel
    {
        ActivityDescription = "Review Submitted",
        ActivityDate = DateTime.Now
    }
};

        return View(model);
    }

}
