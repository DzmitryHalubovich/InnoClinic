using Profiles.Domain.Entities;

namespace Profiles.Domain.Interfaces;

public interface IAccountsRepository
{
    public Task<Account> CreateAsync(Account account);
}
