namespace CareerApplication.Core.Entities;

public class JobEntity : BaseEntity
{
    public static string Node { get => "Jobs"; }
    public string CompanyId { get; set; } = string.Empty;
    public string JobCategoryId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Responsibilities { get; set; } = string.Empty;
    public string Qualifications { get; set; } = string.Empty;
    public string ApplicationDueDate { get; set; } = string.Empty;
}
