using MongoDB.Driver;
using Offices.Domain.Entities;
using Offices.Domain.Interfaces;

namespace Offices.Infrastructure.Repositories;

public class OfficesRepository : IOfficesRepository
{
    private readonly IMongoCollection<Office> _officesCollection;

    public OfficesRepository(IMongoCollection<Office> officesCollection)
    {
        _officesCollection = officesCollection;
    }

    public async Task<List<Office>> GetAllAsync() => 
        await _officesCollection.Find(_ => true).ToListAsync();

    public async Task<Office> GetByIdAsync(string officeId) => 
        await _officesCollection.Find(o => o.Id.Equals(officeId)).FirstOrDefaultAsync();

    public async Task AddNewAsync(Office newOffice) =>
        await _officesCollection.InsertOneAsync(newOffice);

    public async Task DeleteAsync(string officeId) => 
        await _officesCollection.DeleteOneAsync(o => o.Id.Equals(officeId));

    public async Task UpdateAsync(string officeId, Office newOffice) =>
        await _officesCollection.ReplaceOneAsync(o => o.Id == officeId, newOffice);
}
