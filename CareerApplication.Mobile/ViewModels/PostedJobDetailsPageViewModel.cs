namespace CareerApplication.Mobile.ViewModels;

[QueryProperty(nameof(Job), "Job")]
public partial class PostedJobDetailsPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private Job job;

    [ICommand]
    private async Task ApplyAsync()
    {
        await Shell.Current.GoToAsync(nameof(JobApplicationPage), true, new Dictionary<string, object>
        {
            { nameof(Job), Job }
        });
    }
}
