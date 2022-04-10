namespace CareerApplication.Mobile.ViewModels;

public partial class RegistrationPageViewModel : BaseViewModel
{
    #region Properties
    private readonly AuthProvider _auth;
    private readonly DatabaseProvider _db;
    private readonly IMapper _mapper;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string phone;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string confirmPassword;
    #endregion

    #region Constructors
    public RegistrationPageViewModel()
    {}

    public RegistrationPageViewModel(AuthProvider auth, DatabaseProvider db, IMapper mapper)
    {
        _auth = auth;
        _db = db;
        _mapper = mapper;
    }
    #endregion

    #region Private Methods
    [ICommand]
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
                Func<RoleEntity, bool> rolesPredicate = (role) => role.Name == "Job Seeker";
                Func<FirebaseObject<RoleEntity>, RoleEntity> rolesSelector = (role) => _mapper.Map<RoleEntity>(role.Object);
                var role = await _db.GetByIdAsync(RoleEntity.Node, rolesPredicate, rolesSelector);
                var id = await _db.GenerateNewIdAsync<UserEntity>(UserEntity.Node);
                var isAdded = await _db.AddAsync(UserEntity.Node, new UserEntity
                {
                    Id = id,
                    AuthId = result.User.LocalId,
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

    [ICommand]
    private async Task GoBackAsync() => 
        await Shell.Current.GoToAsync("..");

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
