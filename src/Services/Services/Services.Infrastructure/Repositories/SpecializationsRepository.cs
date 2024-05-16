using Microsoft.EntityFrameworkCore;
using Services.Domain.Entities;
using Services.Domain.Interfaces;
using Services.Infrastructure.Data;

namespace Services.Infrastructure.Repositories;

public class SpecializationsRepository : ISpecializationsRepository
{
    private readonly ServicesDbContext _context;

    public SpecializationsRepository(ServicesDbContext context) =>
         _context = context;

    public async Task<List<Specialization>> GetAllAsync() =>
       await _context.Specializations.AsNoTracking().ToListAsync();

    public async Task<Specialization> GetByIdAsync(int id) =>
        await _context.Specializations.AsNoTracking().FirstOrDefaultAsync(s => s.Id.Equals(id));

    public async Task CreateAsync(Specialization newSpecialization)
    {
        _context.Specializations.Add(newSpecialization);

        await _context.SaveChangesAsync();
    }
}
