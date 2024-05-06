using Microsoft.EntityFrameworkCore;
using Profiles.Domain.Entities;

namespace Profiles.Infrastructure.Data;

public class ProfilesDbContext : DbContext
{
    public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : base(options)
    { }

    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Specialization> Specializations { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<PersonalInfo> PersonalInfo { get; set; }
    public DbSet<Patient> Patients { get; set; }
}
