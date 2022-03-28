namespace CareerApplication.Mobile.ViewModels;

[INotifyPropertyChanged]
public partial class ForgotPasswordPageViewModel : BaseViewModel
{
    #region Properties
    private readonly AuthProvider _auth;

    [ObservableProperty]
    private string email;
    #endregion

    #region Constructors
    public ForgotPasswordPageViewModel()
    {}

    public ForgotPasswordPageViewModel(AuthProvider auth)
    {
        _auth = auth;
    }
    #endregion

    #region Private Methods
    [ICommand]
    private async Task ForgetPassword()
    {
        if (string.IsNullOrEmpty(Email))
        {
            await Toast.Make("Please enter email address", ToastDuration.Long).Show();
            return;
        }

        if (!IsValidEmail(Email))
        {
            await Toast.Make("Please enter a valid email", ToastDuration.Long).Show();
            return;
        }

        try
        {
            await _auth.SendPasswordResetEmail(Email);
            await Application.Current.MainPage.DisplayAlert("Success", "Reset password request has been sent. Please check your inbox!", "OK");
            ClearFields();
        }
        catch (FirebaseAuthException e)
        {
            var json = JsonNode.Parse(e.ResponseData);
            var code = json["error"]["message"].GetValue<string>();

            if (code != null && code == "EMAIL_NOT_FOUND")
                await Toast.Make("User not found", ToastDuration.Long).Show();
            else
                await Toast.Make("Unknown error", ToastDuration.Long).Show();
        }
        catch (Exception)
        {
            await Toast.Make("Cannot send password reset request", ToastDuration.Long).Show();
        }
    }

    [ICommand]
    private async Task GoBack() => 
        await Shell.Current.GoToAsync("..");

    private void ClearFields()
    {
        Email = string.Empty;
        OnPropertyChanged();
    }
    #endregion
}
