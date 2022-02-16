namespace CareerApplication.Core.Entities;

public class BaseEntity
{
    public string Id { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public string CreatedBy { get; set; } = string.Empty;

    public string CreatedAt { get; set; } = DateTime.Now.ToString("dd MMM yyyy");

    public string UpdatedBy { get; set; } = string.Empty;

    public string UpdatedAt { get; set; } = string.Empty;
}
