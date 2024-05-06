using Profiles.Domain.Interfaces;
using Profiles.Infrastructure.Data;

namespace Profiles.Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly ProfilesDbContext _context;
    private readonly Lazy<IAccountsRepository> _accountsRepository;
    private readonly Lazy<IDoctorsRepository> _doctorsRepository;
    private readonly Lazy<IPersonalInfoRepository> _personalInfoRepository;
    private readonly Lazy<IPatientsRepository> _patientsRepository;
    
    public RepositoryManager(ProfilesDbContext context)
    {
        _context = context;
        _accountsRepository = new Lazy<IAccountsRepository>(() => new 
            AccountRepository(_context));
        _doctorsRepository = new Lazy<IDoctorsRepository>(() => new 
            DoctorsRepository(_context));
        _personalInfoRepository = new Lazy<IPersonalInfoRepository>(() => new 
            PersonalInfoRepository(_context));
        _patientsRepository = new Lazy<IPatientsRepository>(() => new
            PatientsRepository(_context));
    }

    public IAccountsRepository AccountsRepository => _accountsRepository.Value;
    public IDoctorsRepository DoctorsRepository => _doctorsRepository.Value;
    public IPersonalInfoRepository PersonalInfoRepository => _personalInfoRepository.Value;
    public IPatientsRepository PatientsRepository => _patientsRepository.Value;

    public async Task BeginTransactionAsync() =>
        await _context.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync() =>
        await _context.Database.CommitTransactionAsync();

    public async Task SaveAsync() => 
        await _context.SaveChangesAsync();
}
