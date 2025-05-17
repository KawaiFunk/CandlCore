using System.Text.Json;
using Application.DTOs.Assets;
using Application.Interfaces.Clients.Coinlore;
using Application.Models.Assets;
using Common.Constants;
using Microsoft.Extensions.Options;

namespace Infrastructure.Clients.Coinlore;

public class CoinloreClient : ICoinloreClient
{
    private readonly ICoinloreHttpClientFactory _coinloreHttpClientFactory;
    private readonly ICoinloreUrlBuilder _coinloreUrlBuilder;
    private readonly IOptions<CoinloreOptions> _options;

    public CoinloreClient(
        ICoinloreHttpClientFactory coinloreHttpClientFactory,
        ICoinloreUrlBuilder coinloreUrlBuilder,
        IOptions<CoinloreOptions> options)
    {
        _coinloreHttpClientFactory = coinloreHttpClientFactory;
        _coinloreUrlBuilder = coinloreUrlBuilder;
        _options = options;
    }

    public async Task<CoinloreAssetListModel> GetCryptoAssetsAsync(int startIndex)
    {
        var httpClient = _coinloreHttpClientFactory.GetCoinloreHttpClient();
        var requestUrl = _coinloreUrlBuilder
            .UseAllCoinsEndpoint()
            .AddStart(0)
            .AddLimit(_options.Value.Limit)
            .Build();

        var response = httpClient.GetAsync(requestUrl).Result;
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<CoinloreAssetListModel>(content);
        if (result == null)
        {
            throw new Exception("Failed to deserialize response");
        }

        return result;
    }
}