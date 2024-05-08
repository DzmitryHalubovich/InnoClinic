using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Infrastructure.Data;

namespace Profiles.Infrastructure.Repositories;

public class PersonalInfoRepository : IPersonalInfoRepository
{
    private readonly ProfilesDbContext _context;

    public PersonalInfoRepository(ProfilesDbContext context)
    {
        _context = context;
    }

    public async Task<PersonalInfo> AddPersonalInfoAsync(PersonalInfo accountPersonalInformation)
    {
        _context.PersonalInfo.Add(accountPersonalInformation);
        await _context.SaveChangesAsync();
        return accountPersonalInformation;
    }

    public void UpdatePersonalInfo(PersonalInfo updatedPersonalInfo) => 
        _context.PersonalInfo.Update(updatedPersonalInfo);
}
