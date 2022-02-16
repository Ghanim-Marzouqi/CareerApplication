namespace CareerApplication.Core.Entities;

public class UserTypeEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public UserTypeEntity()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}
