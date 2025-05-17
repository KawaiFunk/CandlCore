using Application.DTOs.Assets;
using Application.Interfaces.Clients.Coinlore;
using Application.Interfaces.Services;
using Infrastructure.Clients.Coinlore;

namespace Infrastructure.Services;

public class CoinloreService : ICoinloreService
{
    private readonly ICoinloreClient _coinloreClient;

    public CoinloreService(ICoinloreClient coinloreClient)
    {
        _coinloreClient = coinloreClient;
    }

    public async Task<List<CoinloreAssetModel>> GetAllCryptoAssetsAsync()
    {
        var assets = await _coinloreClient.GetCryptoAssetsAsync(0);
        if (assets.Data != null)
        {
            return assets.Data;
        }
        
        throw new Exception("Failed to retrieve assets from Coinlore API.");
    }
}