using Profiles.Domain.Entities;

namespace Profiles.Domain.Interfaces;

public interface IReceptionistsRepository
{
    public Task<List<Receptionist>> GetAllAsync(bool trackChanges);

    public Task<Receptionist?> GetByIdAsync(Guid id, bool trackChanges);

    public void Create(Receptionist newReceptionist);

    public void Delete(Receptionist receptionist);
}
