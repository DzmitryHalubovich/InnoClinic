using Microsoft.EntityFrameworkCore;
using Profiles.Contracts.Pagination;
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

    public async Task<List<Doctor>> GetAllAsync(DoctorsQueryParameters queryParameters, bool trackChanges) => !trackChanges
        ? await _context.Doctors.AsNoTracking()
                                .FilterDoctorsBySpecialization(queryParameters.SpecializationId)
                                .Search(queryParameters.SearchFullName)
                                .Include(d => d.Account)
                                .ToListAsync()
        : await _context.Doctors.FilterDoctorsBySpecialization(queryParameters.SpecializationId)
                                .Search(queryParameters.SearchFullName)
                                .Include(d => d.Account)
                                .ToListAsync();

    public async Task<Doctor?> GetByIdAsync(Guid id, bool trackChanges) => !trackChanges
        ? await _context.Doctors.AsNoTracking()
                                .Include(d => d.Account)
                                .FirstOrDefaultAsync(d => d.Id.Equals(id))
        : await _context.Doctors.Include(d => d.Account)
                                .FirstOrDefaultAsync(d => d.Id.Equals(id));

    public void Delete(Doctor doctor) =>
        _context.Doctors.Remove(doctor);
}
