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

    public async Task<Specialization?> GetByIdAsync(int id) =>
        await _context.Specializations.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

    public async Task CreateAsync(Specialization specialization)
    {
        _context.Specializations.Add(specialization);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Specialization specialization)
    {
        _context.Specializations.Update(specialization);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Specialization specialization)
    {
        _context.Specializations.Remove(specialization);

        await _context.SaveChangesAsync();
    }
}
