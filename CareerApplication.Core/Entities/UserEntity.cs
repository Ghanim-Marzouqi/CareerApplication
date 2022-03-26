namespace CareerApplication.Core.Entities;

public class UserEntity : BaseEntity
{
    public static string Node { get => "Users"; }
    public string RoleId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
