namespace CareerApplication.Mobile.Views;

public partial class PostedJobDetailsPage : ContentPage
{
	public PostedJobDetailsPage(PostedJobDetailsPageViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}