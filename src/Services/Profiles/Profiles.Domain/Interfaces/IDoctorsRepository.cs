using Profiles.Domain.Entities;

namespace Profiles.Domain.Interfaces;

public interface IDoctorsRepository
{
    public Task<List<Doctor>> GetAllAsync(Guid? specializationId, string? searchLastName, bool trackChanges);
    public Task<Doctor?> GetByIdAsync(Guid doctorId, bool trackChanges);
    public void Create(Doctor newDoctor);
    public void Update(Doctor editedDoctor);
}
