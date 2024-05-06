using Microsoft.EntityFrameworkCore;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Infrastructure.Data;
using System.Linq.Expressions;

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

    public async Task<List<Doctor>> GetAllAsync(Guid? specializationId, bool trackChanges)
    {
        if (trackChanges)
        {
            var doctors = await _context.Doctors
                                .FilterDoctorsBySpecialization(specializationId)
                                .Include(d => d.Account)
                                .ThenInclude(a => a.PersonalInfo)
                                .Include(d => d.Specialization)
                                .ToListAsync();
            return doctors;
        }
        else
        {
            var doctors = await _context.Doctors.AsNoTracking()
                                .FilterDoctorsBySpecialization(specializationId)
                                .Include(d => d.Account)
                                .ThenInclude(a => a.PersonalInfo)
                                .Include(d => d.Specialization)
                                .ToListAsync();
            return doctors;
        }
    }

    public async Task<Doctor?> GetByIdAsync(Guid doctorId, bool trackChanges) => trackChanges
        ? await _context.Doctors.FirstOrDefaultAsync(d => d.DoctorId.Equals(doctorId))
        : await _context.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.DoctorId.Equals(doctorId));
}
