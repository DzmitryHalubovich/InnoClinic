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
        
        var collectionExists = mongoDb.ListCollectionNames().ToList().Contains(databaseSettings.Value.CollectionName);
        
        if (collectionExists is false)
        {
            mongoDb.CreateCollection(databaseSettings.Value.CollectionName);
        }
    }

    public async Task AddNewAsync(Office newOffice) =>
        await _officesCollection.InsertOneAsync(newOffice);

    public async Task DeleteAsync(string officeId) => 
        await _officesCollection.DeleteOneAsync(x => x.Id.Equals(officeId));

    public async Task<List<Office>> GetAllAsync() => 
        await _officesCollection.Find(_ => true).ToListAsync();

    public async Task<Office> GetByIdAsync(string officeId) => 
        await _officesCollection.Find(x => x.Id.Equals(officeId)).FirstOrDefaultAsync();

    public async Task UpdateAsync(string officeId, Office newOffice)
    {
        var filter = Builders<Office>.Filter.Eq(s => s.Id, officeId);
        var result = await _officesCollection.ReplaceOneAsync(b => b.Id == officeId, newOffice);
    }
}
