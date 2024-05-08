using Profiles.Domain.Entities;

namespace Profiles.Infrastructure.Repositories;

public static class RepositoryDoctorsExtentions
{
    public static IQueryable<Doctor> FilterDoctorsBySpecialization(this IQueryable<Doctor> doctors, Guid? specialization)
        => specialization is null
        ? doctors
        : doctors.Where(d => d.SpecializationId.Equals(specialization));

    public static IQueryable<Doctor> Search(this IQueryable<Doctor> doctors, string searchLastName)
    {
        if (string.IsNullOrWhiteSpace(searchLastName))
        {
            return doctors;
        }

        var loverCaseLastName = searchLastName.Trim().ToLower();

        return doctors.Where(d => d.Account.PersonalInfo.LastName.ToLower().Contains(searchLastName));
    }
}
