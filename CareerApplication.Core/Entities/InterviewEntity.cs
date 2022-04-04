namespace CareerApplication.Core.Entities;

public class InterviewEntity : BaseEntity
{
    public static string Node { get => "Interviews"; }
    public int CompanyId { get; set; }
    public int JobId { get; set; }
    public int[] CandidateIds { get; set; } = Array.Empty<int>();
    public int InterviewType { get; set; } = 1; // 1 => Online, 2 => Offline
    public string InterviewLocation { get; set; } = string.Empty;
    public DateTime InterviewDate { get; set; }
    public TimeSpan InterviewTime { get; set; }
}
