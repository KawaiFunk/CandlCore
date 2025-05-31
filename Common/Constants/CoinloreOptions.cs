namespace Common.Constants;

public class CoinloreOptions
{
    public string BaseUrl       { get; set; } = string.Empty;
    public string AllCoinsUrl   { get; set; } = string.Empty;
    public string SingleCoinUrl { get; set; } = string.Empty;
    public int    BatchSize     { get; set; }
    public int    BatchCount    { get; set; }
}