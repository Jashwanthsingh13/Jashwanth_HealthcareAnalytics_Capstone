using System.Collections.Generic;

namespace HealthcareAnalytics.MVC.Models
{
    public class DashboardViewModel
    {
        public int TotalPatients { get; set; }

        public int TotalAppointments { get; set; }

        public int PendingReviews { get; set; }

        public double Satisfaction { get; set; }

        public List<ActivityViewModel> Activities { get; set; } = new();
        
        public List<AppointmentViewModel> UpcomingAppointments { get; set; }  = new();
    }
}