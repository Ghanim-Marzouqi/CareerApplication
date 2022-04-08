namespace CareerApplication.Core.Models;

public class Interview : InterviewEntity
{
    public User? Company { get; set; }
    public Job? Job { get; set; }
    public IEnumerable<User> Candidates { get; set; } = new List<User>();
}
