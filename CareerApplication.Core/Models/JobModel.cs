namespace CareerApplication.Core.Models;

public class JobModel : JobEntity
{
    public UserModel? Company { get; set; }
    public SectorModel? Sector { get; set; }
    public string JobType { get => JobTypeId == 1 ? "Full Time" : "Part Time"; }
}
