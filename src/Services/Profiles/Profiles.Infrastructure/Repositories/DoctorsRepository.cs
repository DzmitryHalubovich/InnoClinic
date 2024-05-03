using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;

namespace Profiles.Infrastructure.Repositories;

public class DoctorsRepository : IDoctorsRepository
{
    List<Doctor> doctors =
        [
            new()
            {
                DoctorId = new Guid("DA338B92-540C-4261-BF43-EF851EF6267F"),
                FirstName = "Tom",
                LastName = "Jefferson",
                MiddleName = null,
                DateOfBirth = new DateTime(1991, 06, 20),
                CareerStartYear = new DateTime(2020, 11, 21),
                OfficeId = "662e681805655321ed386452",
                Specialization = new()
                {
                    SpecializationId = Guid.NewGuid(),
                    SpecializationName = "Therapist"
                },
                Status = "At work"
            },
            new()
            {
                DoctorId = new Guid("836B65E0-7F64-4CFA-9A9C-E7DD8331841A"),
                FirstName = "Sam",
                LastName = "Bridges",
                MiddleName = null,
                DateOfBirth = new DateTime(1985, 11, 01),
                CareerStartYear = new DateTime(2008, 04, 01),
                OfficeId = "662e6f1acd5d221bc4aee8c5",
                Specialization = new()
                {
                    SpecializationId = Guid.NewGuid(),
                    SpecializationName = "Someone else"
                },
                Status = "Sick Leave"
            },
            new()
            {
                DoctorId = new Guid("3FB64BB1-1632-41DA-BE4B-2BEFCC6DA718"),
                FirstName = "Ivan",
                LastName = "Reon",
                MiddleName = "Anatolievich",
                DateOfBirth = new DateTime(1986, 07, 15),
                CareerStartYear = new DateTime(2010, 02, 01),
                OfficeId = "662e6f1acd5d221bc4aee8c5",
                Specialization = new()
                {
                    SpecializationId = Guid.NewGuid(),
                    SpecializationName = "Intern"
                },
                Status = "Sick Leave"
            }
        ];

    public List<Doctor> GetAll()
    {
        return doctors;
    }

    public Doctor GetById(Guid doctorId)
    {

        return doctors.FirstOrDefault(x=>x.DoctorId.Equals(doctorId));
    }
}
