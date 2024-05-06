namespace Profiles.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        public IAccountsRepository AccountsRepository { get; }
        public IDoctorsRepository DoctorsRepository { get; }
        public IPersonalInfoRepository PersonalInfoRepository { get; }
        public IPatientsRepository PatientsRepository { get; }

        public Task BeginTransactionAsync();
        public Task CommitTransactionAsync();
        public Task SaveAsync();
    }
}
