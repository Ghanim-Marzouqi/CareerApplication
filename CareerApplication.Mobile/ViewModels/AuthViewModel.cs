using System.Text.Json.Nodes;

namespace CareerApplication.Mobile.ViewModels;

public class AuthViewModel : BaseViewModel
{
    #region Properties
    private readonly INavigation _navigation;
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
    public AuthViewModel(INavigation navigation, AuthProvider auth, DatabaseProvider db)
    {
        _navigation = navigation;
        _auth = auth;
        _db = db;

        LoginButtonClicked = new Command(async () => await LoginAsync());
        RegisterButtonClicked = new Command(async () => await RegisterAsync());
        ForgotPasswordButtonClicked = new Command(async () => await ForgetPasswordAsync());
        GoToRegistrationPageButtonClicked = new Command(async () => await navigation.PushAsync(new RegistrationPage(auth, db)));
        GoToForgotPasswordPageButtonClicked = new Command(async () => await navigation.PushAsync(new ForgotPasswordPage(auth, db)));
        GoToHomePageButtonClicked = new Command(async () => await navigation.PushAsync(new HomePage()));
        GoToBackButtonClicked = new Command(async () => await navigation.PopAsync());
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
                // TODO: retrieve user from Firebase Realtime Database and save in shared peferences
                await Toast.Make("Login successfull", ToastDuration.Long).Show();
                await _navigation.PushAsync(new HomePage());
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
                var isAdded = await _db.Add("users", new UserEntity
                {
                    Id = result.User.LocalId,
                    Name = Name,
                    Email = Email,
                    Phone = Phone,
                    CreatedBy = Name
                });

                if (isAdded)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "User registered successfully", "OK");
                    ClearFields();
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Alert", "Cannot register user", "OK");
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

        // TODO: Send reset password request
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
