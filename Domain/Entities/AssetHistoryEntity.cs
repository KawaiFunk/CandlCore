namespace Domain.Entities;

public class AssetHistoryEntity : BaseEntity
{
    public string      AssetId { get; set; } = string.Empty;
    public HistoryData Daily   { get; set; } = new();
    public HistoryData Weekly  { get; set; } = new();
    public HistoryData Monthly { get; set; } = new();
    public HistoryData Yearly  { get; set; } = new();
    public HistoryData All     { get; set; } = new();
}