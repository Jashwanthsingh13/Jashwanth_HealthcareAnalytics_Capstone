using Microsoft.EntityFrameworkCore;
using HealthcareAnalytics.Domain.Entities;
using HealthcareAnalytics.Domain.Interfaces;
using HealthcareAnalytics.Infrastructure.Data;

namespace HealthcareAnalytics.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly HealthcareDbContext _context;

    public AppointmentRepository(HealthcareDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(x => x.Patient)
            .ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(int id)
    {
        return await _context.Appointments
            .Include(x => x.Patient)
            .FirstOrDefaultAsync(x => x.AppointmentId == id);
    }

    public async Task AddAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }
}