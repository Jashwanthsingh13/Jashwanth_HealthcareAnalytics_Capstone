using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using HealthcareAnalytics.API.Models;

namespace HealthcareAnalytics.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public DashboardController(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetDashboardData()
    {
        DashboardDto dashboard = new();

        string connectionString =
    _configuration.GetConnectionString("DefaultConnection")!;

        using (SqlConnection con =
              new SqlConnection(connectionString))
        {
            con.Open();

            SqlCommand cmd1 =
                new SqlCommand(
                "SELECT COUNT(*) FROM Patients",
                con);

            dashboard.TotalPatients =
                (int)cmd1.ExecuteScalar();

            SqlCommand cmd2 =
                new SqlCommand(
                "SELECT COUNT(*) FROM Appointments",
                con);

            dashboard.TotalAppointments =
                (int)cmd2.ExecuteScalar();

            SqlCommand cmd3 =
                new SqlCommand(
                @"SELECT COUNT(*)
                  FROM Reviews
                  WHERE Status='Pending'",
                con);

            dashboard.PendingReviews =
                (int)cmd3.ExecuteScalar();

            SqlCommand cmd4 =
                new SqlCommand(
                @"SELECT ISNULL(AVG(Rating),0)
                  FROM Feedbacks",
                con);

            dashboard.Satisfaction =
                Convert.ToDouble(
                    cmd4.ExecuteScalar()
                ) * 20;

            return Ok(dashboard);
        }
    }
}