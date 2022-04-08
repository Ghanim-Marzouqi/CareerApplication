namespace CareerApplication.Mobile.Views;

public partial class JobApplicationPage : ContentPage
{
	public JobApplicationPage(JobApplicationViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}