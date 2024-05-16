using Profiles.Contracts.Pagination;
using Profiles.Domain.Entities;

namespace Profiles.Domain.Interfaces;

public interface IPatientsRepository
{
    public Task<List<Patient>> GetAllAsync(PatientsQueryParameters queryParameters, bool trackChanges);

    public Task<Patient?> GetByIdAsync(Guid id, bool trackChanges);

    public Task CreateAsync(Patient patient);

    public Task UpdateAsync(Patient patient);

    public Task DeleteAsync(Patient patient);
}
