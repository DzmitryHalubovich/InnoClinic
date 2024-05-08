using Profiles.Domain.Entities;

namespace Profiles.Domain.Interfaces;

public interface IPatientsRepository
{
    public Task<List<Patient>> GetAllAsync(bool trackChanges);
    public Task<Patient?> GetByIdAsync(Guid patientId, bool trackChanges);
    public void Create(Patient newPatient);
    public void Delete(Patient patient);
}
