namespace CareerApplication.Mobile.ViewModels;

public class AppShellViewModel : BaseViewModel
{
    public string UserName { set; get; } = string.Empty;

    public AppShellViewModel()
    {
        UserName = User.Name;
        OnPropertyChanged(nameof(UserName));
    }
}
