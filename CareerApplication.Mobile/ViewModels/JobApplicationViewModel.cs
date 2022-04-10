namespace CareerApplication.Mobile.ViewModels;

[QueryProperty(nameof(Job), "Job")]
public partial class JobApplicationViewModel : BaseViewModel
{
    [ObservableProperty]
    private Job job;

    [ObservableProperty]
    private string cv;

    [ObservableProperty]
    private string covid19Certificate;
}
