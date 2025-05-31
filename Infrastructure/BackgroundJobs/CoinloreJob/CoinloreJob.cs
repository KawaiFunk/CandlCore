using Application.Interfaces.Clients.Coinlore;
using Application.Interfaces.Services;
using Application.Mappers.AssetProfile;
using Common.Constants;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.BackgroundJobs.CoinloreJob;

public class CoinloreJob : ICoinloreJob
{
    private readonly IAssetService             _assetService;
    private readonly ILogger<CoinloreJob>      _logger;
    private readonly ICoinloreClient           _coinloreClient;
    private readonly IAssetMapper              _mapper;
    private readonly IOptions<CoinloreOptions> _options;

    public CoinloreJob(
        ICoinloreClient           coinloreClient,
        IAssetMapper              mapper,
        ILogger<CoinloreJob>      logger,
        IAssetService             assetService,
        IOptions<CoinloreOptions> options)
    {
        _coinloreClient = coinloreClient;
        _mapper         = mapper;
        _logger         = logger;
        _assetService   = assetService;
        _options        = options;
    }

    public async Task GetCryptoAssetsAsync()
    {
        var batchSize = _options.Value.BatchSize;
        var batchCount = _options.Value.BatchCount;

        try
        {
            _logger.LogInformation("Fetching crypto assets from Coinlore...");

            for (var i = 0; i < batchCount; i++)
            {
                var startIndex = i * batchSize;
                var assets = await _coinloreClient.GetCryptoAssetsAsync(startIndex);

                if (assets.Data == null || assets.Data.Count == 0)
                {
                    _logger.LogInformation("No assets found starting from index {StartIndex}", startIndex);
                    continue;
                }

                var upsertTasks = assets.Data.Select(model => _assetService.UpsertAsync(_mapper.ToEntity(model)));
                await Task.WhenAll(upsertTasks);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching crypto assets from Coinlore");
            throw;
        }
    }
}