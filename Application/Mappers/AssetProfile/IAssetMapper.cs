using Application.Models.Assets;
using Domain.Entities;

namespace Application.Mappers.AssetProfile;

public interface IAssetMapper
{
    AssetModel         Map(AssetEntity             entity);
    AssetEntity        ToEntity(CoinloreAssetModel model);
    CoinloreAssetModel ToCoinloreModel(AssetEntity entity);
    void               MapToExisting(AssetEntity   source, AssetEntity target);
}