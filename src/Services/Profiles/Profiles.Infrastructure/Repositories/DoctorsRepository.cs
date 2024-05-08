using Microsoft.EntityFrameworkCore;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Infrastructure.Data;

namespace Profiles.Infrastructure.Repositories;

public class DoctorsRepository : IDoctorsRepository
{
    private readonly ProfilesDbContext _context;
    public DoctorsRepository(ProfilesDbContext context) => 
        _context = context;

    public void Create(Doctor newDoctor) => 
        _context.Doctors.Add(newDoctor);

    public async Task<List<Doctor>> GetAllAsync(Guid? specializationId, string? searchLastName, bool trackChanges) => !trackChanges
        ? await _context.Doctors.AsNoTracking()
                                .FilterDoctorsBySpecialization(specializationId)
                                .Search(searchLastName)
                                .Include(d => d.Account)
                                .ThenInclude(a => a.PersonalInfo)
                                .Include(d => d.Specialization)
                                .ToListAsync()
        : await _context.Doctors.FilterDoctorsBySpecialization(specializationId)
                                .Include(d => d.Account)
                                .ThenInclude(a => a.PersonalInfo)
                                .Include(d => d.Specialization)
                                .ToListAsync();

    public async Task<Doctor?> GetByIdAsync(Guid doctorId, bool trackChanges) => !trackChanges
        ? await _context.Doctors.AsNoTracking()
                                .Include(d => d.Specialization)
                                .Include(d => d.Account)
                                .ThenInclude(a => a.PersonalInfo)
                                .FirstOrDefaultAsync(d => d.DoctorId.Equals(doctorId))
        : await _context.Doctors.Include(d => d.Specialization)
                                .Include(d => d.Account)
                                .ThenInclude(a => a.PersonalInfo)
                                .FirstOrDefaultAsync(d => d.DoctorId.Equals(doctorId));

    public void Update(Doctor editedDoctor) =>
        _context.Doctors.Update(editedDoctor);
}
