namespace CareerApplication.Mobile;

public partial class AppShell : Shell
{
    public UserEntity User { get; set; }

    public AppShell()
    {
        InitializeComponent();

        // Register pages for shell navigation
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
        Routing.RegisterRoute(nameof(ForgotPasswordPage), typeof(ForgotPasswordPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(PostedJobsPage), typeof(PostedJobsPage));
        Routing.RegisterRoute(nameof(PostedJobDetailsPage), typeof(PostedJobDetailsPage));
    }
}
