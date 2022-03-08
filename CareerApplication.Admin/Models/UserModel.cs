namespace CareerApplication.Admin.Models;

public class UserModel : UserEntity
{
    public RoleModel? Role { get; set; }
}
