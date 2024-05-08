using Microsoft.EntityFrameworkCore;
using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Infrastructure.Data;

namespace Profiles.Infrastructure.Repositories;

public class PatientsRepository : IPatientsRepository
{
    private readonly ProfilesDbContext _context;

    public PatientsRepository(ProfilesDbContext context) => 
        _context = context;

    public async Task<List<Patient>> GetAllAsync(bool trackChanges) => !trackChanges
        ? await _context.Patients.AsNoTracking()
            .Include(p => p.Account)
            .ThenInclude(p => p.PersonalInfo)
            .ToListAsync()
        : await _context.Patients
            .Include(p => p.Account)
            .ThenInclude(a => a.PersonalInfo)
            .ToListAsync();

    public void Create(Patient newPatient) =>
        _context.Patients.Add(newPatient);

    public async Task<Patient?> GetByIdAsync(Guid patientId, bool trackChanges) => !trackChanges
        ? await _context.Patients.AsNoTracking()
            .Include(p => p.Account)
            .ThenInclude(a => a.PersonalInfo)
            .FirstOrDefaultAsync(p => p.PatientId.Equals(patientId))
        : await _context.Patients
            .Include(p => p.Account)
            .ThenInclude(a => a.PersonalInfo)
            .FirstOrDefaultAsync(p => p.PatientId.Equals(patientId));

    public void Delete(Patient patient) => 
        _context.Remove(patient);
}
