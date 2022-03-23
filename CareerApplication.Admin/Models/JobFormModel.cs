﻿namespace CareerApplication.Admin.Models;

public class JobFormModel
{
    [Required (ErrorMessage = "Please enter job title")]
    public string Title { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Please select job category")]
    public int Category { get; set; }

    [Required(ErrorMessage = "Please enter job description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter job responsibilities")]
    public string Responsibilities { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter job qualifications")]
    public string Qualifications { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please choose application due date")]
    public DateTime ApplicationDueDate { get; set; }
}
