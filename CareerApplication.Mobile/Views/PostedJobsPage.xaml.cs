namespace CareerApplication.Mobile.Views;

public partial class PostedJobsPage : ContentPage
{
	public PostedJobsPage(PostedJobsPageViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}