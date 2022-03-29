namespace CareerApplication.Core.Entities;

public class SectorEntity : BaseEntity
{
    public static string Node { get => "Sectors"; }
    public string Name { get; set; } = string.Empty;
}
