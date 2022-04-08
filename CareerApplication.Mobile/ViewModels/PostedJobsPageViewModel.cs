namespace CareerApplication.Mobile.ViewModels;

public partial class PostedJobsPageViewModel : BaseViewModel
{
    #region Properties
    private readonly DatabaseProvider _db;
    private readonly IMapper _mapper;

    public ObservableCollection<Job> Jobs { get; } = new();

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private bool image;
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
                int dateComparisonResult = DateTime.Compare(job.ApplicationDueDate, DateTime.Today);

                if (dateComparisonResult >= 0)
                {
                    Func<SectorEntity, bool> sectorPredicate = (sector) => sector.Id == job.SectorId;
                    Func<FirebaseObject<SectorEntity>, SectorEntity> sectorSelector = (sector) => _mapper.Map<SectorEntity>(sector.Object);
                    var sector = await _db.GetById(SectorEntity.Node, sectorPredicate, sectorSelector);

                    Func<UserEntity, bool> userPredicate = (user) => user.Id == job.CompanyId;
                    Func<FirebaseObject<UserEntity>, UserEntity> userSelector = (user) => _mapper.Map<UserEntity>(user.Object);
                    var company = await _db.GetById(UserEntity.Node, userPredicate, userSelector);

                    var postedJob = _mapper.Map<Job>(job);
                    postedJob.Company = _mapper.Map<Core.Models.User>(company);
                    postedJob.Sector = _mapper.Map<Sector>(sector);
                    Jobs.Add(postedJob);
                }
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
