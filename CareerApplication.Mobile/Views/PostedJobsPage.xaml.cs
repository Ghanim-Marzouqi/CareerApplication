namespace CareerApplication.Mobile.Views;

public partial class PostedJobsPage : ContentPage
{
    public PostedJobsPage(PostedJobsPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		var job = e.CurrentSelection.FirstOrDefault() as Job;

		if (job == null)
			return;

		await Shell.Current.GoToAsync(nameof(PostedJobDetailsPage), true, new Dictionary<string, object>
		{
			{ nameof(Job), job }
		});

		((CollectionView)sender).SelectedItem = null;
	}
}