namespace CareerApplication.Mobile.Views;

public partial class ForgotPasswordPage : ContentPage
{
    public ForgotPasswordPage(AuthProvider auth, DatabaseProvider db)
    {
        InitializeComponent();

        BindingContext = new AuthViewModel(auth, db);

        // Remove default Appbar
        NavigationPage.SetHasNavigationBar(this, false);
    }
}