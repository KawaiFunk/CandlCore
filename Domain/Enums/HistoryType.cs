namespace Domain.Enums;

public enum HistoryType
{
    Daily, //144 entries per day (10 minutes interval)
    Weekly, //168 entries per week (1 hour interval)
    Monthly, //120 entries per month (6 hours interval)
    Yearly, //363 entries per year (1 day interval)
    All
}