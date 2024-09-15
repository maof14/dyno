namespace Models;

public class BaseEntity
{
    public DateTimeOffset DateTimeChanged { get; set; }
    public string ChangedByUser { get; set; } = string.Empty;
}
