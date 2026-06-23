using Microsoft.AspNetCore.Mvc;
using HealthcareAnalytics.MVC.Models;

namespace HealthcareAnalytics.MVC.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {

        if (model.Username == "admin"
           &&
           model.Password == "Admin123")
        {
            HttpContext.Session
            .SetString("User", "Admin");

            return RedirectToAction(
                "Index",
                "Dashboard");
        }

        ViewBag.Error =
        "Invalid Username Or Password";

        return View(model);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction(
            "Login");
    }
}