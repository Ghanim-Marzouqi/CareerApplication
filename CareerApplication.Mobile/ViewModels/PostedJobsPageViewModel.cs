namespace CareerApplication.Mobile.ViewModels;

[INotifyPropertyChanged]
public partial class PostedJobsPageViewModel : BaseViewModel
{
    #region Properties
    private readonly DatabaseProvider _db;
    private readonly IMapper _mapper;

    public ObservableCollection<JobModel> Jobs { get; } = new();

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private bool isBusy;
    #endregion

    #region Constructors
    public PostedJobsPageViewModel(DatabaseProvider db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;

        Task.Run(() => GetJobsAsync()).Wait();
    }
    #endregion

    #region Methods
    [ICommand]
    private async Task GetJobsAsync()
    {
        try
        {
            IsBusy = true;
            Func<FirebaseObject<JobEntity>, JobEntity> selector = (job) => _mapper.Map<JobEntity>(job.Object);
            var jobs = await _db.GetAll(JobEntity.Node, selector);

            Jobs.Clear();

            foreach (var job in jobs)
            {
                var postedJob = _mapper.Map<JobModel>(job);
                Jobs.Add(postedJob);
            }
                
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Unable to get jobs: {e.Message}");
            await Application.Current.MainPage.DisplayAlert("Error!", e.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }
    #endregion
}
