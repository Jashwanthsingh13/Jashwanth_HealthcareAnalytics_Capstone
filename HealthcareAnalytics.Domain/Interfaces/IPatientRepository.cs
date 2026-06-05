using HealthcareAnalytics.Domain.Entities;

namespace HealthcareAnalytics.Domain.Interfaces;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllAsync();

    Task<Patient?> GetByIdAsync(int id);

    Task AddAsync(Patient patient);

    Task UpdateAsync(Patient patient);

    Task DeleteAsync(int id);
}