using Microsoft.EntityFrameworkCore;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Infrastructure.Data;

namespace Profiles.Infrastructure.Repositories;

public class ReceptionistsRepository : IReceptionistsRepository
{
    private readonly ProfilesDbContext _context;
    
    public ReceptionistsRepository(ProfilesDbContext context) => 
        _context = context;

    public async Task<List<Receptionist>> GetAllAsync(bool trackChanges) => !trackChanges
        ? await _context.Receptionists.AsNoTracking()
            .Include(r => r.Account)
            .ThenInclude(a => a.PersonalInfo)
            .ToListAsync()
        : await _context.Receptionists
            .Include(r => r.Account)
            .ThenInclude(a => a.PersonalInfo)
            .ToListAsync();

    public async Task<Receptionist?> GetByIdAsync(Guid receptionistId, bool trackChanges) => !trackChanges
        ? await _context.Receptionists.AsNoTracking()
            .Include(r => r.Account)
            .ThenInclude(a => a.PersonalInfo)
            .FirstOrDefaultAsync(r => r.ReceptionistId.Equals(receptionistId))
        : await _context.Receptionists
            .Include(r => r.Account)
            .ThenInclude(a => a.PersonalInfo)
            .FirstOrDefaultAsync(r => r.ReceptionistId.Equals(receptionistId));

    public void Create(Receptionist newReceptionist) =>
        _context.Receptionists.Add(newReceptionist);

    public void Delete(Receptionist receptionist) =>
        _context.Receptionists.Remove(receptionist);
}
