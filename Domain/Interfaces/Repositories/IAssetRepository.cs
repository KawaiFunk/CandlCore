using Domain.Entities;
using Domain.Interfaces.Repositories.Generic;

namespace Domain.Interfaces.Repositories;

public interface IAssetRepository : IGenericRepository<AssetEntity>
{
    Task<AssetEntity> GetByExternalIdAsync(string externalId);
}