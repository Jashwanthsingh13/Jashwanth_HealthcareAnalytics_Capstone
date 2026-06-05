using Microsoft.AspNetCore.Mvc;
using HealthcareAnalytics.Domain.Entities;
using HealthcareAnalytics.Domain.Interfaces;

namespace HealthcareAnalytics.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentRepository _repository;

    public AppointmentsController(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Appointment appointment)
    {
        await _repository.AddAsync(appointment);

        return Ok(appointment);
    }
}