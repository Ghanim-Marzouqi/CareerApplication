namespace CareerApplication.Mobile.ViewModels;

public class RegistrationPageViewModel : ViewModelBase
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
    #endregion

    #region Commands
    public ICommand RegisterButtonClicked { get; set; }
    public ICommand GoToBackButtonClicked { get; set; }
    #endregion

    #region Constructors
    public RegistrationPageViewModel(AuthProvider auth, DatabaseProvider db)
    {
        _auth = auth;
        _db = db;

        RegisterButtonClicked = new Command(async () => await RegisterAsync());
        GoToBackButtonClicked = new Command(async () => await Navigation.PopAsync());
    }
    #endregion

    #region Private Methods
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
                Func<RoleEntity, bool> predicate = (userType) => userType.Name == "Applicant";
                Func<FirebaseObject<RoleEntity>, RoleEntity> selector = (role) =>
                    new RoleEntity
                    {
                        Id = role.Object.Id,
                        Name = role.Object.Name,
                        CreatedBy = role.Object.CreatedBy,
                        CreatedAt = role.Object.CreatedAt
                    };
                var role = _db.GetById(RoleEntity.Node, predicate, selector);
                var isAdded = await _db.Add(UserEntity.Node, new UserEntity
                {
                    Id = result.User.LocalId,
                    RoleId = role.Id,
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
