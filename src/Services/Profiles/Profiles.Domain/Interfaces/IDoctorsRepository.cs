using Profiles.Domain.Entities;

namespace Profiles.Domain.Interfaces;

public interface IDoctorsRepository
{
    public Task<List<Doctor>> GetAllAsync(bool trackChanges);
    public Task<Doctor?> GetByIdAsync(Guid doctorId, bool trackChanges);
    public Task<Doctor?> CreateAsync(Doctor newDoctor);
}
