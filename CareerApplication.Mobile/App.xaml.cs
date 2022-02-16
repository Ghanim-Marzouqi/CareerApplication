namespace CareerApplication.Mobile
{
    public partial class App : Application
    {
        public App(AuthProvider auth, DatabaseProvider db)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage(auth, db));
        }
    }
}
