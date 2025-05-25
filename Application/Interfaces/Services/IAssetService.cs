using Domain.Entities;

namespace Application.Interfaces.Services;

public interface IAssetService : IGenericService<AssetEntity>
{
    Task UpsertAsync(AssetEntity entity);
}