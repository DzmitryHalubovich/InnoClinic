using Services.Domain.Entities;

namespace Services.Domain.Interfaces;

public interface ISpecializationsRepository
{
    public Task<List<Specialization>> GetAllAsync();

    public Task<Specialization> GetByIdAsync(int id);

    public Task CreateAsync(Specialization newSpecialization);
}
