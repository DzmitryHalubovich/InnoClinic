using Profiles.Domain.Entities;
using System.Linq.Expressions;

namespace Profiles.Domain.Interfaces;

public interface IDoctorsRepository
{
    public Task<List<Doctor>> GetAllAsync(Guid? specializationId, bool trackChanges);
    public Task<Doctor?> GetByIdAsync(Guid doctorId, bool trackChanges);
    public Task<Doctor?> CreateAsync(Doctor newDoctor);
}
