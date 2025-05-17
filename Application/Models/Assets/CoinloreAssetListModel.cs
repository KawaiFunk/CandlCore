using System.Text.Json.Serialization;
using Application.DTOs.Assets;

namespace Application.Models.Assets;

public class CoinloreAssetListModel
{
    [JsonPropertyName("data")]
    public List<CoinloreAssetModel>? Data { get; set; }
}