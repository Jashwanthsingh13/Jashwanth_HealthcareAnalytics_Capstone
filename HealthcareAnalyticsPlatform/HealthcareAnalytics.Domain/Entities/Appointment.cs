namespace HealthcareAnalytics.Domain.Entities;

public class Appointment
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public Patient Patient { get; set; } = null!;
}