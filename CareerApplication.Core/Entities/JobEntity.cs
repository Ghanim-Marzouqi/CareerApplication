namespace CareerApplication.Core.Entities;

public class JobEntity : BaseEntity
{
    public static string Node { get => "Jobs"; }
    public string CompanyId { get; set; } = string.Empty;
    public string SectorId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Responsibilities { get; set; } = string.Empty;
    public string Qualifications { get; set; } = string.Empty;
    public string ApplicationDueDate { get; set; } = string.Empty;
    public string InterviewType { get; set; } = string.Empty;
    public string InterviewLocation { get; set; } = string.Empty;
    public string JobLocation { get; set; } = string.Empty;
    public string InterviewDate { get; set; } = string.Empty;
    public string InterviewTime { get; set; } = string.Empty;
}
