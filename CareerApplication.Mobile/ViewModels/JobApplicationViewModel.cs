namespace CareerApplication.Mobile.ViewModels;

[QueryProperty(nameof(Job), "Job")]
public partial class JobApplicationViewModel : BaseViewModel
{
    [ObservableProperty]
    private Job job;
}
