namespace CareerApplication.Mobile.Views;

public partial class JobApplicationPage : ContentPage
{
	public JobApplicationPage(JobApplicationViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

    private void PickCv_Clicked(object sender, EventArgs e)
    {

    }

    private void PickCovid19Certificate_Clicked(object sender, EventArgs e)
    {

    }
}