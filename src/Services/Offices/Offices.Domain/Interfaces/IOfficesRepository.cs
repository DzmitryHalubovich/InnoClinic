using Offices.Domain.Entities;

namespace Offices.Domain.Interfaces;

public interface IOfficesRepository
{
    public Task<List<Office>> GetAllAsync();
    public Task AddNewAsync(Office newOffice);
}
