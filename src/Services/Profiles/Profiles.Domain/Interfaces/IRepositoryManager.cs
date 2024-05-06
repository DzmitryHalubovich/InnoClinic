namespace Profiles.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        public IAccountsRepository AccountRepository { get; }
        public IDoctorsRepository DoctorsRepository { get; }
        public IPersonalInfoRepository PersonalInfoRepository { get; }

        public Task SaveAsync();
    }
}
