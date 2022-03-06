namespace CareerApplication.Mobile.Views;

public partial class ForgotPasswordPage : ContentPage
{
    public ForgotPasswordPage(AuthProvider auth)
    {
        InitializeComponent();

        BindingContext = new ForgotPasswordPageViewModel(auth);
    }
}