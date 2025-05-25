using System.Collections.ObjectModel;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Generic;
using MongoDB.Driver.Linq;

namespace Infrastructure.Repositories;

public class AssetRepository(MongoDbContext context) : GenericRepository<AssetEntity>(context), IAssetRepository
{
    public async Task<AssetEntity> GetByExternalIdAsync(string externalId)  
    {
        return await Table.FirstOrDefaultAsync(a => a.ExternalId == externalId);
    }
}