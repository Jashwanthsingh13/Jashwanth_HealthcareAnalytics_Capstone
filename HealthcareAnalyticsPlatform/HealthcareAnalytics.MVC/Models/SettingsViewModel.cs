namespace HealthcareAnalytics.MVC.Models;

public class SettingsViewModel
{
    public string Username { get; set; }
        = string.Empty;

    public string Email { get; set; }
        = string.Empty;

    public string Role { get; set; }
        = string.Empty;

    public int TotalUsers { get; set; }

    public int TotalDoctors { get; set; }

    public int TotalPatients { get; set; }
}