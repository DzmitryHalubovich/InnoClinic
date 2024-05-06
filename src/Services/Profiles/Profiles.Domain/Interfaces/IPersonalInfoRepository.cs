using Profiles.Domain.Entities;

namespace Profiles.Domain.Interfaces;

public interface IPersonalInfoRepository
{
    public Task<PersonalInformation> AddPersonalInfoAsync(PersonalInformation accountPersonalInformation);
}
