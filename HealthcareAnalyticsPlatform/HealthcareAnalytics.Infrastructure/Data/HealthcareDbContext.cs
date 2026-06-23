using Microsoft.EntityFrameworkCore;
using HealthcareAnalytics.Domain.Entities;

namespace HealthcareAnalytics.Infrastructure.Data;

public class HealthcareDbContext : DbContext
{
    public HealthcareDbContext(
        DbContextOptions<HealthcareDbContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();

    public DbSet<Appointment> Appointments => Set<Appointment>();

    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();
}