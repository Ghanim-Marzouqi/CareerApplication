namespace CareerApplication.Mobile.ViewModels;

public class AuthViewModel : BaseViewModel
{
    #region Properties
    private readonly AuthProvider _auth;
    private readonly DatabaseProvider _db;

    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

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

    private string _phone;
    public string Phone
    {
        get => _phone;
        set
        {
            _phone = value;
            OnPropertyChanged(nameof(Phone));
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

    private string _confirmPassword;
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set
        {
            _confirmPassword = value;
            OnPropertyChanged(nameof(ConfirmPassword));
        }
    }

    private bool _rememberMe;
    public bool RememberMe { 
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
    public ICommand RegisterButtonClicked { get; set; }
    public ICommand ForgotPasswordButtonClicked { get; set; }
    public ICommand GoToRegistrationPageButtonClicked { get; set; }
    public ICommand GoToForgotPasswordPageButtonClicked { get; set; }
    public ICommand GoToHomePageButtonClicked { get; set; }
    public ICommand GoToBackButtonClicked { get; set; }
    #endregion

    #region Constructors
    public AuthViewModel(AuthProvider auth, DatabaseProvider db)
    {
        _auth = auth;
        _db = db;

        LoginButtonClicked = new Command(async () => await LoginAsync());
        RegisterButtonClicked = new Command(async () => await RegisterAsync());
        ForgotPasswordButtonClicked = new Command(async () => await ForgetPasswordAsync());
        GoToRegistrationPageButtonClicked = new Command(async () => await Navigation.PushAsync(new RegistrationPage(auth, db)));
        GoToForgotPasswordPageButtonClicked = new Command(async () => await Navigation.PushAsync(new ForgotPasswordPage(auth, db)));
        GoToHomePageButtonClicked = new Command(async () => await Navigation.PushAsync(new HomePage()));
        GoToBackButtonClicked = new Command(async () => await Navigation.PopAsync());

        // Get credetials if stored
        var credentails = GetData<LoginModel>("credentials");

        if (credentails != null)
        {
            Email = credentails.Email;
            Password = credentails.Password;
            RememberMe = credentails.RememberMe;
        }
    }
    #endregion

    #region Methods
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

    private async Task RegisterAsync()
    {
        // Validate user input
        if (string.IsNullOrEmpty(Name))
        {
            await Toast.Make("Please enter name", ToastDuration.Long).Show();
            return;
        }

        if (string.IsNullOrEmpty(Email))
        {
            await Toast.Make("Please enter email address", ToastDuration.Long).Show();
            return;
        }

        if (string.IsNullOrEmpty(Phone))
        {
            await Toast.Make("Please enter phone number", ToastDuration.Long).Show();
            return;
        }

        if (string.IsNullOrEmpty(Password))
        {
            await Toast.Make("Please enter password", ToastDuration.Long).Show();
            return;
        }

        if (string.IsNullOrEmpty(ConfirmPassword))
        {
            await Toast.Make("Please confirm password", ToastDuration.Long).Show();
            return;
        }

        if (!IsValidEmail(Email))
        {
            await Toast.Make("Please enter a valid email", ToastDuration.Long).Show();
            return;
        }

        if (!IsValidPhone(Phone))
        {
            await Toast.Make("Please enter a valid phone", ToastDuration.Long).Show();
            return;
        }

        if (!ConfirmPassword.Equals(Password))
        {
            await Toast.Make("Passwords don't match", ToastDuration.Long).Show();
            return;
        }

        try
        {
            var result = await _auth.CreateUserWithEmailAndPassword(Email, Password);

            if (result != null && result.User != null)
            {
                Func<UserTypeEntity, bool> predicate = (userType) => userType.Name == "Applicant";
                Func<FirebaseObject<UserTypeEntity>, UserTypeEntity> selector = (userType) => 
                    new UserTypeEntity 
                    { 
                        Id = userType.Object.Id, 
                        Name = userType.Object.Name,
                        CreatedBy = userType.Object.CreatedBy,
                        CreatedAt = userType.Object.CreatedAt
                    };
                var userType = _db.GetById(UserTypeEntity.Node, predicate, selector);
                var isAdded = await _db.Add(UserEntity.Node, new UserEntity
                {
                    Id = result.User.LocalId,
                    UserTypeId = userType.Id,
                    Name = Name,
                    Email = Email,
                    Phone = Phone,
                    CreatedBy = Name
                });

                if (isAdded)
                {
                    // Send email verification request
                    await _auth.SendEmailVerification(result.FirebaseToken);
                    await Application.Current.MainPage.DisplayAlert("Success", "User registered successfully", "OK");
                    ClearFields();
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Failure", "Cannot register user", "OK");
            }
            else
            {
                await Toast.Make("Cannot register user", ToastDuration.Long).Show();
            }
        }
        catch (FirebaseAuthException e)
        {
            var json = JsonNode.Parse(e.ResponseData);
            var code = json["error"]["message"].GetValue<string>();

            if (code != null && code == "EMAIL_EXISTS")
                await Toast.Make("User already exists", ToastDuration.Long).Show();
            else
                await Toast.Make("Unknown error", ToastDuration.Long).Show();
        }
        catch (Exception)
        {
            await Toast.Make("Unknown Error", ToastDuration.Long).Show();
        }
        
    }

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
        Name = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Password = string.Empty;
        ConfirmPassword = string.Empty;
        OnPropertyChanged();
    }
    #endregion
}
