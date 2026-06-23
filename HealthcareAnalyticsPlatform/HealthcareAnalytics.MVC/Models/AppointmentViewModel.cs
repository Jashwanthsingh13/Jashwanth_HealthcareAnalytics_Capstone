namespace HealthcareAnalytics.MVC.Models;

public class AppointmentViewModel
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public string PatientName { get; set; }
        = string.Empty;

    public string DoctorName { get; set; }
        = string.Empty;

    public DateTime AppointmentDate { get; set; }

    public string Status { get; set; }
        = "Scheduled";

    public string Notes { get; set; }
        = string.Empty;
}