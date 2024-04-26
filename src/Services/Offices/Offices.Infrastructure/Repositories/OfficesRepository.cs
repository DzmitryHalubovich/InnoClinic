using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Offices.Domain.Entities;
using Offices.Domain.Interfaces;

namespace Offices.Infrastructure.Repositories;

public class OfficesRepository : IOfficesRepository
{
    private readonly IMongoCollection<Office> _officesCollection;

    public OfficesRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _officesCollection = mongoDb.GetCollection<Office>(databaseSettings.Value.CollectionName);
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
