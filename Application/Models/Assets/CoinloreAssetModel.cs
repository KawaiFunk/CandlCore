namespace Application.Models.Assets;

public class CoinloreAssetModel
{
    public string  Id       { get; set; } = string.Empty;
    public string  Symbol           { get; set; } = string.Empty;
    public string  Name             { get; set; } = string.Empty;
    public int     Rank             { get; set; }
    public string  PriceUsd         { get; set; } = string.Empty;
    public string  PercentChange24h { get; set; }
    public string  PercentChange1h  { get; set; }
    public string  PercentChange7d  { get; set; }
    public string  PriceBtc         { get; set; }
    public string  MarketCapUsd     { get; set; }
    public decimal Volume24a        { get; set; }
}