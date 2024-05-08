using Profiles.Domain.Entities;

namespace Profiles.Domain.Interfaces;

public interface IPersonalInfoRepository
{
    public Task<PersonalInfo?> AddPersonalInfoAsync(PersonalInfo accountPersonalInformation);

    public void UpdatePersonalInfo(PersonalInfo updatedPersonalInfo);
}
