namespace CareerApplication.Mobile.Views;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage(AuthProvider auth, DatabaseProvider db)
    {
        InitializeComponent();

        BindingContext = new RegistrationPageViewModel(auth, db);
    }
}