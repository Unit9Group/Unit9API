using AutoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AutoApi.Services;

public class MotorcycleService
{
    private readonly IMongoCollection<Motorcycle> _motorcycleCollection;

    public MotorcycleService(
        IOptions<AutoDatabaseSettings> autoDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            autoDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            autoDatabaseSettings.Value.DatabaseName);

        _motorcycleCollection = mongoDatabase.GetCollection<Motorcycle>(
            autoDatabaseSettings.Value.AutoCollectionName);
    }

    public async Task<List<Motorcycle>> GetAsync() =>
        await _motorcycleCollection.Find(_ => true).ToListAsync();

    public async Task<Motorcycle?> GetAsync(string id) =>
        await _motorcycleCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Motorcycle newMotorcycle) =>
        await _motorcycleCollection.InsertOneAsync(newMotorcycle);

    public async Task UpdateAsync(string id, Motorcycle updatedMotorcycle) =>
        await _motorcycleCollection.ReplaceOneAsync(x => x.Id == id, updatedMotorcycle);

    public async Task RemoveAsync(string id) =>
        await _motorcycleCollection.DeleteOneAsync(x => x.Id == id);
}