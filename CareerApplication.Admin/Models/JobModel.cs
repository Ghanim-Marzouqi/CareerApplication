namespace CareerApplication.Admin.Models;

public class JobModel : JobEntity
{
    public JobCategoryModel? JobCategory { get; set; }
}
