using Profiles.Domain.Entities;

namespace Profiles.Domain.Interfaces;

public interface IDoctorsRepository
{
    public List<Doctor> GetAll();
    public Doctor GetById(Guid doctorId);
}
