namespace CareerApplication.Core.Models;

public class InterviewModel : InterviewEntity
{
    public UserModel? Company { get; set; }
    public JobModel? Job { get; set; }
    public IEnumerable<UserModel> Candidates { get; set; } = new List<UserModel>();
}
