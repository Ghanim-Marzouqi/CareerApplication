namespace CareerApplication.Core.Entities;

public class JobApplicationEntity : BaseEntity
{
    public static string Node { get => "JobApplications"; }
    public int JobId { get; set; }
    public int JobSeekerId { get; set; }
}
