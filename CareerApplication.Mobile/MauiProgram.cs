namespace CareerApplication.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var firebaseSettings = new FirebaseSettings();
        var builder = MauiApp.CreateBuilder();
        var firebaseApiKey = new FirebaseConfig(firebaseSettings.ApiKey);
        var firebaseJson = JsonSerializer.Serialize(firebaseSettings);

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddSingleton(_ => new AuthProvider(
            new FirebaseAuthProvider(firebaseApiKey)
        ));

        builder.Services.AddSingleton(_ => new DatabaseProvider(
            new FirebaseClient(firebaseSettings.RealtimeDatabaseUrl)
        ));

        return builder.Build();
    }
}
