using Application.Interfaces.Mediator;
using Application.Interfaces.Services;
using Application.Mappers.AssetProfile;
using Application.Models.Assets;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.AssetHandlers;

public class GetAssetByIdRequestHandler : IRequestHandler<GetAssetByIdRequest, AssetModel>
{
    private readonly IAssetService                       _assetService;
    private readonly IAssetMapper                        _assetMapper;
    private readonly ILogger<GetAssetByIdRequestHandler> _logger;

    public GetAssetByIdRequestHandler(
        IAssetService                       assetService,
        IAssetMapper                        assetMapper,
        ILogger<GetAssetByIdRequestHandler> logger)
    {
        _assetService = assetService;
        _assetMapper  = assetMapper;
        _logger       = logger;
    }

    public async Task<AssetModel> HandleAsync(GetAssetByIdRequest request)
    {
        try
        {
            _logger.LogInformation("Fetching asset by ID: {Id}", request.Id);
            var asset = await _assetService.GetByIdAsync(request.Id);

            if (asset == null)
            {
                _logger.LogWarning("Asset with ID {Id} not found", request.Id);
                throw new KeyNotFoundException($"Asset with ID {request.Id} not found.");
            }

            return _assetMapper.Map(asset);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting asset by ID: {Id}", request.Id);
            throw;
        }
    }
}

public class GetAssetByIdRequest(string id) : IRequest<AssetModel>
{
    public string Id { get; } = id;
}