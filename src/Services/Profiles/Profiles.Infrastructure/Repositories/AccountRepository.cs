using Profiles.Domain.Entities;
using Profiles.Domain.Interfaces;
using Profiles.Infrastructure.Data;

namespace Profiles.Infrastructure.Repositories;

public class AccountRepository : IAccountsRepository
{
    private readonly ProfilesDbContext _context;

    public AccountRepository(ProfilesDbContext context)
    {
        _context = context;
    }

    public void Create(Account account) => 
        _context.Accounts.Add(account);
}
