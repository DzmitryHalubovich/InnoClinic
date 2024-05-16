using Profiles.Domain.Entities;

namespace Profiles.Domain.Interfaces;

public interface IReceptionistsRepository
{
    public Task<List<Receptionist>> GetAllAsync(bool trackChanges);

    public Task<Receptionist?> GetByIdAsync(Guid id, bool trackChanges);

    public Task CreateAsync(Receptionist receptionist);

    public Task UpdateAsync(Receptionist receptionist);

    public Task DeleteAsync(Receptionist receptionist);
}
