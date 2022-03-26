namespace CareerApplication.Mobile.Views;

public partial class ForgotPasswordPage : ContentPage
{
    public ForgotPasswordPage(ForgotPasswordPageViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}