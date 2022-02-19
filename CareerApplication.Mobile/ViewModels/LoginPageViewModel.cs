namespace CareerApplication.Mobile.ViewModels;

public class LoginPageViewModel : ViewModelBase
{
    #region Properties
    private readonly AuthProvider _auth;
    private readonly DatabaseProvider _db;

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

    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    private bool _rememberMe;
    public bool RememberMe
    {
        get => _rememberMe;
        set
        {
            _rememberMe = value;
            OnPropertyChanged(nameof(RememberMe));
        }
    }
    #endregion

    #region Commands
    public ICommand LoginButtonClicked { get; set; }
    public ICommand GoToRegistrationPageButtonClicked { get; set; }
    public ICommand GoToForgotPasswordPageButtonClicked { get; set; }
    public ICommand GoToHomePageButtonClicked { get; set; }
    #endregion

    #region Constructors
    public LoginPageViewModel(AuthProvider auth, DatabaseProvider db)
    {
        _auth = auth;
        _db = db;

        LoginButtonClicked = new Command(async () => await LoginAsync());
        GoToRegistrationPageButtonClicked = new Command(async () => await Navigation.PushAsync(new RegistrationPage(auth, db)));
        GoToForgotPasswordPageButtonClicked = new Command(async () => await Navigation.PushAsync(new ForgotPasswordPage(auth)));
        GoToHomePageButtonClicked = new Command(async () => await Navigation.PushAsync(new HomePage()));

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
        catch (Exception)
        {
            // TODO: Display error to user
        }
        
    }
    #endregion

    #region Private Methods
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
                Func<UserEntity, bool> predicate = (user) => user.Id == result.User.LocalId;
                Func<FirebaseObject<UserEntity>, UserEntity> selector = (user) =>
                    new UserEntity
                    {
                        Id = user.Object.Id,
                        Name = user.Object.Name,
                        Email = user.Object.Email,
                        Phone = user.Object.Phone,
                        IsActive = user.Object.IsActive,
                        CreatedBy = user.Object.CreatedBy,
                        CreatedAt = user.Object.CreatedAt,
                        UpdatedBy = user.Object.UpdatedBy,
                        UpdatedAt = user.Object.UpdatedAt
                    };
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
                await Navigation.PushAsync(new HomePage());
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
        catch (Exception)
        {
            await Toast.Make("Cannot send login request", ToastDuration.Long).Show();
        }
    }

    private void ClearFields()
    {
        Email = string.Empty;
        Password = string.Empty;
        RememberMe = false;
        OnPropertyChanged();
    }
    #endregion
}
