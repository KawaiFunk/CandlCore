namespace Application.Interfaces.Clients.Coinlore;

public interface ICoinloreUrlBuilder
{
    ICoinloreUrlBuilder UseAllCoinsEndpoint();
    ICoinloreUrlBuilder UseSingleCoinEndpoint();
    ICoinloreUrlBuilder AddStart(int startIndex);
    ICoinloreUrlBuilder AddLimit(int limit);
    ICoinloreUrlBuilder AddAssetId(string assetId);
    string Build();
}