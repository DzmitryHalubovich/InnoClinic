using Profiles.Domain.Entities;

namespace Profiles.Infrastructure.Repositories;

public static class RepositoryDoctorsExtentions
{
    public static IQueryable<Doctor> FilterDoctorsBySpecialization(this IQueryable<Doctor> doctors, Guid? specialization)
        => specialization is null
        ? doctors
        : doctors.Where(d => d.SpecializationId.Equals(specialization));
}
