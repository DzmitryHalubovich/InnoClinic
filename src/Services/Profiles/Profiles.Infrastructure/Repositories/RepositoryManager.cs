using Profiles.Domain.Interfaces;
using Profiles.Infrastructure.Data;

namespace Profiles.Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly ProfilesDbContext _context;
    private readonly Lazy<IAccountsRepository> _accountsRepository;
    private readonly Lazy<IDoctorsRepository> _doctorsRepository;
    private readonly Lazy<IPatientsRepository> _patientsRepository;
    private readonly Lazy<IReceptionistsRepository> _receptionistsRepository;
    
    public RepositoryManager(ProfilesDbContext context)
    {
        _context = context;
        _accountsRepository = new Lazy<IAccountsRepository>(() => new 
            AccountRepository(_context));
        _doctorsRepository = new Lazy<IDoctorsRepository>(() => new 
            DoctorsRepository(_context));
        _patientsRepository = new Lazy<IPatientsRepository>(() => new
            PatientsRepository(_context));
        _receptionistsRepository = new Lazy<IReceptionistsRepository>(() => new
            ReceptionistsRepository(_context));
    }

    public IAccountsRepository AccountsRepository => _accountsRepository.Value;

    public IDoctorsRepository DoctorsRepository => _doctorsRepository.Value;

    public IPatientsRepository PatientsRepository => _patientsRepository.Value;

    public IReceptionistsRepository ReceptionistsRepository => _receptionistsRepository.Value;

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}
