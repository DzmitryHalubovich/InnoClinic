using Profiles.Contracts.Pagination;
using Profiles.Domain.Entities;

namespace Profiles.Domain.Interfaces;

public interface IDoctorsRepository
{
    public Task<List<Doctor>> GetAllAsync(DoctorsQueryParameters queryParameters, bool trackChanges);

    public Task<Doctor?> GetByIdAsync(Guid id, bool trackChanges);

    public void Create(Doctor newDoctor);

    public void Delete(Doctor doctor);
}
