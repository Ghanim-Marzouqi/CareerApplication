namespace CareerApplication.Mobile.ViewModels;

public partial class LoginPageViewModel : BaseViewModel
{
    #region Properties
    private readonly AuthProvider _auth;
    private readonly DatabaseProvider _db;
    private readonly IMapper _mapper;

    [ObservableProperty]
    [AlsoNotifyCanExecuteFor(nameof(LoginCommand))]
    private string email;

    [ObservableProperty]
    [AlsoNotifyCanExecuteFor(nameof(LoginCommand))]
    private string password;

    [ObservableProperty]
    private bool rememberMe;
    #endregion

    #region Constructors
    public LoginPageViewModel(AuthProvider auth, DatabaseProvider db, IMapper mapper)
    {
        _auth = auth;
        _db = db;
        _mapper = mapper;

        try
        {
            // Get credetials if stored
            var credentails = GetData<LoginModel>("credentials");

            if (credentails != null)
            {
                Email = credentails.Email;
                Password = credentails.Password;
                RememberMe = credentails.RememberMe;
            }
        }
        catch (Exception e)
        {
           Debug.WriteLine($"LoginViewModel Exception: {e.Message}");
        }
        
    }
    #endregion

    #region Private Methods
    [ICommand]
    private async Task LoginAsync()
    {
        // Validate user input
        if (string.IsNullOrEmpty(Email))
        {
            await Toast.Make("Please enter email address", ToastDuration.Long).Show();
            return;
        }

        if (string.IsNullOrEmpty(Password))
        {
            await Toast.Make("Please enter password", ToastDuration.Long).Show();
            return;
        }

        if (!IsValidEmail(Email))
        {
            await Toast.Make("Please enter a valid email", ToastDuration.Long).Show();
            return;
        }

        try
        {
            var result = await _auth.SignInWithEmailAndPassword(Email, Password);

            if (result != null && result.User != null)
            {
                // Retieve user data from database
                Func<UserEntity, bool> predicate = (user) => user.AuthId == result.User.LocalId;
                Func<FirebaseObject<UserEntity>, UserEntity> selector = (user) => _mapper.Map<UserEntity>(user.Object);
                var loggedInUser = await _db.GetById(UserEntity.Node, predicate, selector);

                if (loggedInUser == null)
                {
                    await Toast.Make("User data not found", ToastDuration.Long).Show();
                    return;
                }

                StoreData("user", loggedInUser);

                if (RememberMe)
                    StoreData("credentials", new LoginModel { Email = Email, Password = Password, RememberMe = RememberMe });
                else
                    RemoveData("credentials");

                await Toast.Make("Login successfull", ToastDuration.Long).Show();
                await Shell.Current.GoToAsync($"//{nameof(PostedJobsPage)}");
                ClearFields();
            }
            else
            {
                await Toast.Make("Wrong email or password", ToastDuration.Long).Show();
            }
        }
        catch (FirebaseAuthException e)
        {
            var json = JsonNode.Parse(e.ResponseData);
            var code = json["error"]["message"].GetValue<string>();

            if (code != null && code == "EMAIL_NOT_FOUND")
                await Toast.Make("User not found", ToastDuration.Long).Show();
            else if (code != null && code == "INVALID_PASSWORD")
                await Toast.Make("Wrong password", ToastDuration.Long).Show();
            else
                await Toast.Make("Unknown error", ToastDuration.Long).Show();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            await Toast.Make("Cannot send login request", ToastDuration.Long).Show();
        }
    }

    [ICommand]
    async Task GoToRegistrationPageAsync() =>
        await Shell.Current.GoToAsync(nameof(RegistrationPage));

    [ICommand]
    async Task GoToForgotPasswordPageAsync() =>
        await Shell.Current.GoToAsync(nameof(ForgotPasswordPage));

    private void ClearFields()
    {
        Email = string.Empty;
        Password = string.Empty;
        RememberMe = false;
        OnPropertyChanged();
    }
    #endregion
}
