namespace CareerApplication.Mobile.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    public bool Not(bool value) => !value;
}