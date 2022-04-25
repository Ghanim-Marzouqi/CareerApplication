namespace CareerApplication.Mobile.ViewModels;

[QueryProperty(nameof(Job), "Job")]
public partial class PostedJobDetailsPageViewModel : BaseViewModel
{
    private readonly DatabaseProvider _db;

    [ObservableProperty]
    private Job job;

    public PostedJobDetailsPageViewModel(DatabaseProvider db)
    {
        _db = db;
    }

    [ICommand]
    private async Task ApplyAsync()
    {
        if (Job == null)
            return;

        try
        {
            var loggedInUser = GetData<UserEntity>("user");

            if (loggedInUser == null)
            {
                await Toast.Make("User data not found", ToastDuration.Long).Show();
                return;
            }

            var id = await _db.GenerateNewIdAsync<JobApplicationEntity>(JobApplicationEntity.Node);
            var isAdded = await _db.AddAsync(JobApplicationEntity.Node, new JobApplicationEntity
            {
                Id = id,
                JobId = Job.Id,
                JobSeekerId = loggedInUser.Id,
                CreatedBy = loggedInUser.Name,
                CreatedAt = DateTime.Now.ToString("dd MMM yyyy"),
                IsActive = true
            });

            if (isAdded)
            {
                await Application.Current.MainPage.DisplayAlert("Success", $"You have applied to {Job.Title} job successfully", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
                await Application.Current.MainPage.DisplayAlert("Failure", "Cannot apply to this job", "OK");
        }
        catch (Exception)
        {
            await Toast.Make("Unknown Error", ToastDuration.Long).Show();
        }
    }
}
