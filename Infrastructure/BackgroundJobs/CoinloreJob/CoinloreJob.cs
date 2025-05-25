using Application.Interfaces.Clients.Coinlore;
using Application.Interfaces.Services;
using Application.Mappers.AssetProfile;
using Microsoft.Extensions.Logging;

namespace Infrastructure.BackgroundJobs.CoinloreJob;

public class CoinloreJob : ICoinloreJob
{
    private readonly IAssetService        _assetService;
    private readonly ILogger<CoinloreJob> _logger;
    private readonly ICoinloreClient      _coinloreClient;
    private readonly IAssetMapper         _mapper;

    public CoinloreJob(
        ICoinloreClient      coinloreClient,
        IAssetMapper         mapper,
        ILogger<CoinloreJob> logger,
        IAssetService        assetService)
    {
        _coinloreClient = coinloreClient;
        _mapper         = mapper;
        _logger         = logger;
        _assetService   = assetService;
    }

    public async Task GetCryptoAssetsAsync()
    {
        for (var i = 0; i < 3; i++)
        {
            var startIndex = i * 100;
            var assets = await _coinloreClient.GetCryptoAssetsAsync(startIndex);

            if (assets.Data == null || assets.Data.Count == 0)
            {
                _logger.LogInformation("No assets found starting from index {StartIndex}", startIndex);
                continue;
            }

            foreach (var mappedAsset in assets.Data.Select(model => _mapper.Map(model)))
            {
                await _assetService.UpsertAsync(mappedAsset);
            }
        }
    }
}