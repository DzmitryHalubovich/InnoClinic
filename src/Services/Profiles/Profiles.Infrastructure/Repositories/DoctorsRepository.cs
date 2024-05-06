using Microsoft.EntityFrameworkCore;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Infrastructure.Data;

namespace Profiles.Infrastructure.Repositories;

public class DoctorsRepository : IDoctorsRepository
{
    private readonly ProfilesDbContext _context;
    public DoctorsRepository(ProfilesDbContext context)
    {
        _context = context;
    }

    public async Task<Doctor?> CreateAsync(Doctor newDoctor)
    {
        _context.Doctors.Add(newDoctor);
        await _context.SaveChangesAsync();
        return newDoctor;
    }

    public async Task<List<Doctor>> GetAllAsync(Guid? specializationId, bool trackChanges) => trackChanges
        ? await _context.Doctors.FilterDoctorsBySpecialization(specializationId)
                                .Include(d => d.Account)
                                .ThenInclude(a => a.PersonalInfo)
                                .Include(d => d.Specialization)
                                .ToListAsync()
        : await _context.Doctors.AsNoTracking()
                                .FilterDoctorsBySpecialization(specializationId)
                                .Include(d => d.Account)
                                .ThenInclude(a => a.PersonalInfo)
                                .Include(d => d.Specialization)
                                .ToListAsync();
    
    public async Task<Doctor?> GetByIdAsync(Guid doctorId, bool trackChanges) => trackChanges
        ? await _context.Doctors.FirstOrDefaultAsync(d => d.DoctorId.Equals(doctorId))
        : await _context.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.DoctorId.Equals(doctorId));
}
