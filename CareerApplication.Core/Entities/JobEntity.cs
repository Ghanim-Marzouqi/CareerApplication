namespace CareerApplication.Core.Entities;

public class JobEntity : BaseEntity
{
    public static string Node { get => "Jobs"; }
    public int CompanyId { get; set; }
    public int JobCategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Responsibilities { get; set; } = string.Empty;
    public string Qualifications { get; set; } = string.Empty;
    public DateTime ApplicationDueDate { get; set; }
}
