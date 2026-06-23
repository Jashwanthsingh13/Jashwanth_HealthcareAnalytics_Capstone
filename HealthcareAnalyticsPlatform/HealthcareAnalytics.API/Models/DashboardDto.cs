namespace HealthcareAnalytics.API.Models;

public class DashboardDto
{
    public int TotalPatients { get; set; }

    public int TotalAppointments { get; set; }

    public int PendingReviews { get; set; }

    public double Satisfaction { get; set; }
}