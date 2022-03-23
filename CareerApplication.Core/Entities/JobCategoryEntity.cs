namespace CareerApplication.Core.Entities;

public class JobCategoryEntity : BaseEntity
{
    public static string Node { get => "JobCategories"; }
    public string Name { get; set; } = string.Empty;
}
