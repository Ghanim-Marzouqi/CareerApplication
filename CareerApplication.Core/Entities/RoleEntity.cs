namespace CareerApplication.Core.Entities;

public class RoleEntity : BaseEntity
{
    public static string Node { get => "Roles"; }
    public string Name { get; set; } = string.Empty;
}