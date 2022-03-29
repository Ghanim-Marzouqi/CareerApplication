namespace CareerApplication.Admin.Models;

public class JobFormModel
{
    [Required(ErrorMessage = "Please enter job title")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select job sector")]
    public string SectorId { get; set; } = "select_job_sector";

    [Required(ErrorMessage = "Please enter job description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter job responsibilities")]
    public string Responsibilities { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter job qualifications")]
    public string Qualifications { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please choose application due date")]
    public DateTime? ApplicationDueDate { get; set; }

    [Required(ErrorMessage = "Please select interview type")]
    public string InterviewType { get; set; } = "select_interview_type";

    [Required(ErrorMessage = "Please enter interview location")]
    public string InterviewLocation { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter job location")]
    public string JobLocation { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please choose interview date")]
    public DateTime? InterviewDate { get; set; }

    [Required(ErrorMessage = "Please choose interview time")]
    public TimeSpan? InterviewTime { get; set; }
}
