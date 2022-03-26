namespace CareerApplication.Core.Models;

public class UserModel : UserEntity
{
    public RoleModel? Role { get; set; }
}
