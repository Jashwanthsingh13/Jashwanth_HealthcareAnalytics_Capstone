using System.ComponentModel.DataAnnotations;

namespace HealthcareAnalytics.MVC.Models;

public class PatientViewModel
{
    public int PatientId { get; set; }

    [Required]
    public string FullName { get; set; } = "";

    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    public string PhoneNumber { get; set; } = "";

    public DateTime DateOfBirth { get; set; }

    public string Gender { get; set; } = "";
}