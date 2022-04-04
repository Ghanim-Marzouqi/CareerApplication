namespace CareerApplication.Admin.Models;

public class InterviewFormModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Please select interview type")]
    public int InterviewType { get; set; }

    [Required(ErrorMessage = "Please enter interview location")]
    public string InterviewLocation { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please choose interview date")]
    public DateTime? InterviewDate { get; set; }

    [Required(ErrorMessage = "Please choose interview time")]
    public TimeSpan? InterviewTime { get; set; }
}
      