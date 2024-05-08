using Profiles.Domain.Entities;

namespace Profiles.Domain.Interfaces;

public interface IAccountsRepository
{
    public void Create(Account newAccount);
}
