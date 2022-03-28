namespace CareerApplication.Mobile.ViewModels;

[INotifyPropertyChanged]
public partial class AppShellViewModel : BaseViewModel
{
    [ICommand]
    private async Task Logout() =>
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
}
