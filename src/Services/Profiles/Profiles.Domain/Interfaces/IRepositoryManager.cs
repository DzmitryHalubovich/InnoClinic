namespace Profiles.Domain.Interfaces;

public interface IRepositoryManager
{
    public IAccountsRepository AccountsRepository { get; }

    public IDoctorsRepository DoctorsRepository { get; }

    public IPatientsRepository PatientsRepository { get; }

    public IReceptionistsRepository ReceptionistsRepository { get; }

    public Task SaveAsync();
}
