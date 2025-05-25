using Application.Models.Assets;

namespace Application.Interfaces.Clients.Coinlore;

public interface ICoinloreClient
{
    Task<CoinloreAssetListModel> GetCryptoAssetsAsync(int startIndex);
}