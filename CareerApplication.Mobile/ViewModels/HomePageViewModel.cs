namespace CareerApplication.Mobile.ViewModels;

public class HomePageViewModel : ViewModelBase
{
    public UserEntity User { get; set; }

    public HomePageViewModel()
    {
        User = GetData<UserEntity>("user");

        if (User != null)
            User.Name = $"Welcome, {User.Name}";
    }
}
