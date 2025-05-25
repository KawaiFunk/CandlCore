using Application.Models.Assets;
using Domain.Entities;

namespace Application.Mappers.AssetProfile;

public interface IAssetMapper
{
    AssetEntity        Map(CoinloreAssetModel model);
    CoinloreAssetModel Map(AssetEntity        entity);
    void               MapToExisting(AssetEntity    source, AssetEntity target);
}