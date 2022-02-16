namespace CareerApplication.Mobile.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();

        // Remove default Appbar
        NavigationPage.SetHasNavigationBar(this, false);
    }
}