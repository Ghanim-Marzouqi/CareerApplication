namespace CareerApplication.Mobile.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(AuthProvider auth, DatabaseProvider db)
    {
        InitializeComponent();

        BindingContext = new AuthViewModel(auth, db);

        // Remove default Appbar
        NavigationPage.SetHasNavigationBar(this, false);
    }
}