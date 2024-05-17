using Microsoft.EntityFrameworkCore;
using Services.Domain.Entities;
using Services.Domain.Interfaces;
using Services.Infrastructure.Data;

namespace Services.Infrastructure.Repositories;

public class ServicesRepository : IServicesRepository
{
    private readonly ServicesDbContext _context;

    public ServicesRepository(ServicesDbContext context) =>
        _context = context;

    public async Task<List<Service>> GetAllActiveAsync() =>
        await _context.Services.AsNoTracking()
            .Where(x => x.Status.Equals(Status.Active) && x.Specialization.Status.Equals(Status.Active))
            .Include(x => x.ServiceCategory)
            .Include(x => x.Specialization)
            .ToListAsync();

    public async Task<Service?> GetByIdAsync(Guid id) =>
        await _context.Services.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

    public async Task CreateAsync(Service service)
    {
        _context.Services.Add(service);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Service service)
    {
        _context.Services.Update(service);

        await _context.SaveChangesAsync();
    }
}
