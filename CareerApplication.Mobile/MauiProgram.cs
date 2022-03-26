namespace CareerApplication.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        var firebaseSettings = new FirebaseSettings();
        var firebaseApiKey = new FirebaseConfig(firebaseSettings.ApiKey);

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("MaterialDesignIcons.ttf", "IconFontTypes");
            });

        // Register AutoMapper
        var mappingConfig = new MapperConfiguration(config => { config.AddProfile<MappingService>(); });
        IMapper mapper = mappingConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);

        // Register Firebase Providers
        builder.Services.AddSingleton(_ => new AuthProvider(new FirebaseAuthProvider(firebaseApiKey)));
        builder.Services.AddSingleton(_ => new DatabaseProvider(new FirebaseClient(firebaseSettings.RealtimeDatabaseUrl)));

        // Register ViewModels
        builder.Services.AddSingleton<BaseViewModel>();
        builder.Services.AddSingleton<AppShellViewModel>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<RegistrationPageViewModel>();
        builder.Services.AddSingleton<ForgotPasswordPageViewModel>();
        builder.Services.AddSingleton<HomePageViewModel>();
        builder.Services.AddSingleton<ProfilePageViewModel>();
        builder.Services.AddSingleton<AboutPageViewModel>();
        builder.Services.AddSingleton<SettingsPageViewModel>();
        builder.Services.AddSingleton<PostedJobsPageViewModel>();
        builder.Services.AddSingleton<PostedJobDetailsPageViewModel>();

        // Register Views
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<RegistrationPage>();
        builder.Services.AddSingleton<ForgotPasswordPage>();
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<ProfilePage>();
        builder.Services.AddSingleton<AboutPage>();
        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<PostedJobsPage>();
        builder.Services.AddSingleton<PostedJobDetailsPage>();

        return builder.Build();
    }
}
