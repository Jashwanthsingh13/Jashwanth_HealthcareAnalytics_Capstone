using System.Security.Claims;
using HealthcareAnalytics.Domain.Entities;
using HealthcareAnalytics.Infrastructure.Data;
using HealthcareAnalytics.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareAnalytics.MVC.Controllers;

public class AuthController : Controller
{
    private readonly HealthcareDbContext _dbContext;
    private readonly PasswordHasher<User> _passwordHasher = new();
    private static readonly IReadOnlyList<(string Username, string Email, string Password, string Role)> DemoAccounts =
    [
        ("admin", "admin@healthcare.local", "Admin@123", "Admin"),
        ("doctor", "doctor@healthcare.local", "Doctor@123", "Doctor"),
        ("patient", "patient@healthcare.local", "Patient@123", "Patient")
    ];

    public AuthController(HealthcareDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var normalizedRole = model.Role.Trim();
        if (await TrySignInFromDatabaseAsync(model, normalizedRole))
        {
            return RedirectToAction(GetDashboardAction(normalizedRole));
        }

        if (TrySignInFromDemoAccounts(model, normalizedRole))
        {
            return RedirectToAction(GetDashboardAction(normalizedRole));
        }

        ModelState.AddModelError(string.Empty, "Invalid username, password, or role.");
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminDashboard()
    {
        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Doctor")]
    public IActionResult DoctorDashboard()
    {
        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Patient")]
    public IActionResult PatientDashboard()
    {
        return View();
    }

    private string GetDashboardAction(string roleName)
    {
        return roleName switch
        {
            "Doctor" => nameof(DoctorDashboard),
            "Patient" => nameof(PatientDashboard),
            _ => nameof(AdminDashboard)
        };
    }

    private async Task<bool> TrySignInFromDatabaseAsync(LoginViewModel model, string normalizedRole)
    {
        try
        {
            var user = await _dbContext.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x =>
                    x.Username == model.Username || x.Email == model.Username);

            if (user == null || user.Role.RoleName != normalizedRole)
            {
                return false;
            }

            var passwordResult = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                model.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
            {
                return false;
            }

            return await SignInAsync(user.UserId.ToString(), user.Username, user.Email, user.Role.RoleName);
        }
        catch
        {
            return false;
        }
    }

    private bool TrySignInFromDemoAccounts(LoginViewModel model, string normalizedRole)
    {
        var account = DemoAccounts.FirstOrDefault(x =>
            string.Equals(x.Role, normalizedRole, StringComparison.OrdinalIgnoreCase) &&
            (string.Equals(x.Username, model.Username, StringComparison.OrdinalIgnoreCase) ||
             string.Equals(x.Email, model.Username, StringComparison.OrdinalIgnoreCase)) &&
            string.Equals(x.Password, model.Password, StringComparison.Ordinal));

        if (account == default)
        {
            return false;
        }

        SignInAsync("0", account.Username, account.Email, account.Role).GetAwaiter().GetResult();
        return true;
    }

    private async Task<bool> SignInAsync(string userId, string username, string email, string roleName)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, roleName)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        return true;
    }
}