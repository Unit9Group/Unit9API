using AutoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AutoApi.Services;

public class TruckService
{
    private readonly IMongoCollection<Truck> _trucksCollection;

    public TruckService(
        IOptions<AutoDatabaseSettings> vehiclesDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            vehiclesDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            vehiclesDatabaseSettings.Value.DatabaseName);

        _trucksCollection = mongoDatabase.GetCollection<Truck>(
            vehiclesDatabaseSettings.Value.TrucksCollectionName);
    }

    public async Task<List<Truck>> GetAsync() =>
        await _trucksCollection.Find(_ => true).ToListAsync();

    public async Task<Truck?> GetAsync(string id) =>
        await _trucksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Truck newTruck) =>
        await _trucksCollection.InsertOneAsync(newTruck);

    public async Task UpdateAsync(string id, Truck updatedTruck) =>
        await _trucksCollection.ReplaceOneAsync(x => x.Id == id, updatedTruck);

    public async Task RemoveAsync(string id) =>
        await _trucksCollection.DeleteOneAsync(x => x.Id == id);
}