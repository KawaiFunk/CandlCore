using System.Text.Json.Serialization;

namespace Application.DTOs.Assets;

public class CoinloreAssetModel
{
    [JsonPropertyName("id")]
    public string ExternalId { get; set; } = string.Empty;

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("price_usd")]
    public string PriceUsd { get; set; } = string.Empty;

    [JsonPropertyName("percent_change_24h")]
    public string PercentChange24h { get; set; }

    [JsonPropertyName("percent_change_1h")]
    public string PercentChange1h { get; set; }

    [JsonPropertyName("percent_change_7d")]
    public string PercentChange7d { get; set; }

    [JsonPropertyName("price_btc")]
    public string PriceBtc { get; set; }

    [JsonPropertyName("market_cap_usd")]
    public string MarketCapUsd { get; set; }

    [JsonPropertyName("volume24a")]
    public decimal Volume24a { get; set; }
}