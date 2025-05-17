using Application.Interfaces.Clients.Coinlore;
using Common.Constants;
using Microsoft.Extensions.Options;

namespace Infrastructure.Clients.Coinlore;

public class CoinloreHttpClientFactory : ICoinloreHttpClientFactory
{
    private readonly IOptionsMonitor<CoinloreOptions> _options;

    public CoinloreHttpClientFactory(IOptionsMonitor<CoinloreOptions> options)
    {
        _options = options;
    }

    public HttpClient GetCoinloreHttpClient()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_options.CurrentValue.BaseUrl);
        return httpClient;
    }
}