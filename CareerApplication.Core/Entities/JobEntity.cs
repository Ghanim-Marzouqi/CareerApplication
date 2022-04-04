namespace CareerApplication.Core.Entities;

public class JobEntity : BaseEntity
{
    public static string Node { get => "Jobs"; }
    public int CompanyId { get; set; }
    public int SectorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Responsibilities { get; set; } = string.Empty;
    public string Qualifications { get; set; } = string.Empty;
    public DateTime ApplicationDueDate { get; set; }
    public int JobTypeId { get; set; } = 1; // 1 => Full Time, 2 => Part Time
    public string JobLocation { get; set; } = string.Empty;
    public int NumberOfVacancies { get; set; }
}