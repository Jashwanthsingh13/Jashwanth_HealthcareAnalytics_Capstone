namespace HealthcareAnalytics.MVC.Models;

public class DoctorViewModel
{
    public int DoctorId { get; set; }

    public string DoctorName { get; set; }
        = string.Empty;

    public string Specialization { get; set; }
        = string.Empty;

    public string Email { get; set; }
        = string.Empty;

    public string PhoneNumber { get; set; }
        = string.Empty;
}