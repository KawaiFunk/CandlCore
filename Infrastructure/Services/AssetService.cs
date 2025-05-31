using Application.Interfaces.Services;
using Application.Mappers.AssetProfile;
using Domain.Common.PagedList;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Generic;

namespace Infrastructure.Services;

public class AssetService(
    IGenericRepository<AssetEntity> repository,
    IAssetMapper                    mapper,
    IAssetRepository                assetRepository)
    : GenericService<AssetEntity>(repository), IAssetService
{
    private readonly IAssetRepository _assetRepository = assetRepository;
    private readonly IAssetMapper     _mapper          = mapper;

    public async Task UpsertAsync(AssetEntity entity)
    {
        var existingAsset = await _assetRepository.GetByExternalIdAsync(entity.ExternalId);

        if (existingAsset == null)
        {
            await _assetRepository.AddAsync(entity);
        }
        else
        {
            _mapper.MapToExisting(entity, existingAsset);
            await _assetRepository.UpdateAsync(existingAsset);
        }
    }

    public new async Task<IPagedList<AssetEntity>> GetAllAsync(PagedListFilter filter)
    {
        return await _assetRepository.GetAllAsync(filter);
    }
}