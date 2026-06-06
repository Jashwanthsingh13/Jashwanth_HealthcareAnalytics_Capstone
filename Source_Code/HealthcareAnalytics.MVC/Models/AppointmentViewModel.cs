using System.ComponentModel.DataAnnotations;

namespace HealthcareAnalytics.MVC.Models;

public class AppointmentViewModel
{
    public int AppointmentId { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }

    public string Status { get; set; } = "";

    public string? Notes { get; set; }
}