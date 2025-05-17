using Application.DTOs.Assets;

namespace Application.Interfaces.Services;

public interface ICoinloreService
{
    public Task<List<CoinloreAssetModel>> GetAllCryptoAssetsAsync();
}