using System.Text.Json;
using Application.Interfaces.Clients.Coinlore;
using Application.Models.Assets;
using Common.Constants;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Clients.Coinlore;

public class CoinloreClient : ICoinloreClient
{
    private readonly ICoinloreHttpClientFactory _coinloreHttpClientFactory;
    private readonly ICoinloreUrlBuilder        _coinloreUrlBuilder;
    private readonly IOptions<CoinloreOptions>  _options;
    private readonly ILogger<CoinloreClient>    _logger;

    public CoinloreClient(
        ICoinloreHttpClientFactory coinloreHttpClientFactory,
        ICoinloreUrlBuilder        coinloreUrlBuilder,
        IOptions<CoinloreOptions>  options,
        ILogger<CoinloreClient>    logger)
    {
        _coinloreHttpClientFactory = coinloreHttpClientFactory;
        _coinloreUrlBuilder        = coinloreUrlBuilder;
        _options                   = options;
        _logger                    = logger;
    }

    public async Task<CoinloreAssetListModel> GetCryptoAssetsAsync(int startIndex)
    {
        try
        {
            _logger.LogInformation("Getting assets from index {Index}", startIndex);
            
            var httpClient = _coinloreHttpClientFactory.GetCoinloreHttpClient();
            if (httpClient == null)
            {
                _logger.LogError("Failed to create Coinlore HTTP client");
                throw new Exception("Coinlore HTTP client is null");
            }

            var requestUrl = _coinloreUrlBuilder
                .UseAllCoinsEndpoint()
                .AddStart(startIndex)
                .AddLimit(_options.Value.BatchSize)
                .Build();

            var response = await httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<CoinloreAssetListModel>(content,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy        = JsonNamingPolicy.SnakeCaseLower,
                    PropertyNameCaseInsensitive = true
                }
            );
            if (result == null)
            {
                throw new Exception("Failed to deserialize response");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching crypto assets from Coinlore");
            throw new Exception("Error fetching crypto assets from Coinlore", ex);
        }
    }
}