using Domain.Entities;
using Infrastructure.Data;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class AssetRepository
{
    private readonly IMongoCollection<AssetEntity> _collection;

    public AssetRepository(MongoDbContext context)
    {
        _collection = context.GetCollection<AssetEntity>(nameof(AssetEntity));
    }

    public async Task AddAsync(AssetEntity asset) => 
        await _collection.InsertOneAsync(asset); 
}