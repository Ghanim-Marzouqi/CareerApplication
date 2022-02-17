namespace CareerApplication.Mobile.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public UserEntity User { get; set; }

    public HomeViewModel()
    {
        User = GetData<UserEntity>("user");

        if (User != null)
            User.Name = $"Welcome, {User.Name}";
    }
}
