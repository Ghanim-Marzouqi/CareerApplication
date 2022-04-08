namespace CareerApplication.Core.Models;

public class Job : JobEntity
{
    public User? Company { get; set; }
    public Sector? Sector { get; set; }
    public string JobType => JobTypeId == 1 ? "Full Time" : "Part Time";
    public string Image => JobTypeId == 1 ? "full_time_job.png" : "part_time_job.png";
    public string DueDate => ApplicationDueDate.ToString("dd MMM yyyy");
}
