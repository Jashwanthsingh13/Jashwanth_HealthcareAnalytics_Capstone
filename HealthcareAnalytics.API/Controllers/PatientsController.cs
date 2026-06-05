using Microsoft.AspNetCore.Mvc;
using HealthcareAnalytics.Domain.Entities;
using HealthcareAnalytics.Domain.Interfaces;

namespace HealthcareAnalytics.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientRepository _repository;

    public PatientsController(IPatientRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await _repository.GetByIdAsync(id);

        if (patient == null)
            return NotFound();

        return Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Patient patient)
    {
        await _repository.AddAsync(patient);

        return Ok(patient);
    }
}