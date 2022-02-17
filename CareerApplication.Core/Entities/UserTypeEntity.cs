namespace CareerApplication.Core.Entities;

public class UserTypeEntity : BaseEntity
{
    public static string Node { get => "UserTypes"; }
    public string Name { get; set; } = string.Empty;
}