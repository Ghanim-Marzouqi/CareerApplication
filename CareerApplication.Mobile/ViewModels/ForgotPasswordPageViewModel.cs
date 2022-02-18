namespace CareerApplication.Mobile.ViewModels;

public class ForgotPasswordPageViewModel : ViewModelBase
{
    #region Properties
    private readonly AuthProvider _auth;

    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }
    #endregion

    #region Commands
    public ICommand ForgotPasswordButtonClicked { get; set; }
    public ICommand GoToBackButtonClicked { get; set; }
    #endregion

    #region Constructors
    public ForgotPasswordPageViewModel(AuthProvider auth)
    {
        _auth = auth;

        ForgotPasswordButtonClicked = new Command(async () => await ForgetPasswordAsync());
        GoToBackButtonClicked = new Command(async () => await Navigation.PopAsync());
    }
    #endregion

    #region Private Methods
    private async Task ForgetPasswordAsync()
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

    private void ClearFields()
    {
        Email = string.Empty;
        OnPropertyChanged();
    }
    #endregion
}
