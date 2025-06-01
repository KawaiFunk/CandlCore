using Domain.Enums;

namespace Domain.Entities;

public class HistoryData
{
    public HistoryType          Type { get; set; }
    public List<HistoryElement> Data { get; set; } = new();
}