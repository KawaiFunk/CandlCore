using Application.Interfaces.Mediator;
using Application.Interfaces.Services;
using Application.Mappers.AssetProfile;
using Application.Models.Assets;
using Domain.Common.PagedList;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.AssetHandlers;

public class GetAssetsRequestHandler : IRequestHandler<GetAssetsRequest, IPagedList<AssetModel>>
{
    private readonly ILogger<GetAssetsRequestHandler> _logger;
    private readonly IAssetService                    _assetService;
    private readonly IAssetMapper                     _assetMapper;

    public GetAssetsRequestHandler(
        ILogger<GetAssetsRequestHandler> logger,
        IAssetService                    assetService,
        IAssetMapper                     assetMapper)
    {
        _logger       = logger;
        _assetService = assetService;
        _assetMapper  = assetMapper;
    }

    public async Task<IPagedList<AssetModel>> HandleAsync(GetAssetsRequest request)
    {
        try
        {
            _logger.LogInformation("Fetching all assets paged");
            var assets = await _assetService.GetAllAsync(request.Filter);

            return assets.ToMappedPaged(_assetMapper.Map);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}

public class GetAssetsRequest(PagedListFilter filter) : IRequest<IPagedList<AssetModel>>
{
    public PagedListFilter Filter { get; } = filter;
}