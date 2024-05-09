using Profiles.Contracts.Pagination;
using Profiles.Domain.Entities;

namespace Profiles.Domain.Interfaces;

public interface IPatientsRepository
{
    public Task<List<Patient>> GetAllAsync(PatientsQueryParameters queryParameters, bool trackChanges);

    public Task<Patient?> GetByIdAsync(Guid id, bool trackChanges);

    public void Create(Patient newPatient);

    public void Delete(Patient patient);
}
