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

    public async Task AddNewAsync(Office newOffice)
    {
        await _officesCollection.InsertOneAsync(newOffice);
    }

    public async Task<List<Office>> GetAllAsync()
    {
        var offices = await _officesCollection.Find(_ => true).ToListAsync();

        return offices;
    }
}
