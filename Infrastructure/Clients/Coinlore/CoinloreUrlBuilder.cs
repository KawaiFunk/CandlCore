using System.Text;
using Application.Interfaces.Clients.Coinlore;
using Common.Constants;
using Microsoft.Extensions.Options;

namespace Infrastructure.Clients.Coinlore;

public class CoinloreUrlBuilder : ICoinloreUrlBuilder
{
    private readonly IOptions<CoinloreOptions> _options;
    private readonly StringBuilder             _builder         = new();
    private          bool                      _hasQueryStarted = false;

    public CoinloreUrlBuilder(IOptions<CoinloreOptions> options)
    {
        _options = options;
    }

    public ICoinloreUrlBuilder UseAllCoinsEndpoint()
    {
        _builder.Clear();
        _hasQueryStarted = false;
        _builder.Append(_options.Value.BaseUrl);
        _builder.Append(_options.Value.AllCoinsUrl);
        return this;
    }
    
    public ICoinloreUrlBuilder UseSingleCoinEndpoint()
    {
        _builder.Clear();
        _hasQueryStarted = false;
        _builder.Append(_options.Value.BaseUrl);
        _builder.Append(_options.Value.SingleCoinUrl);
        return this;
    }

    public ICoinloreUrlBuilder AddStart(int startIndex)
    {
        AddQueryParam("start", startIndex.ToString());
        return this;
    }

    public ICoinloreUrlBuilder AddLimit(int limit)
    {
        AddQueryParam("limit", limit.ToString());
        return this;
    }

    public ICoinloreUrlBuilder AddAssetId(string assetId)
    {
        AddQueryParam("id", assetId);
        return this;
    }

    public string Build()
    {
        return _builder.ToString();
    }

    private void AddQueryParam(string key, string value)
    {
        if (!_hasQueryStarted)
        {
            _builder.Append('?');
            _hasQueryStarted = true;
        }
        else
        {
            _builder.Append('&');
        }

        _builder.Append($"{key}={value}");
    }
}