﻿namespace CareerApplication.Core.Models;

public class JobModel : JobEntity
{
    public JobCategoryModel? JobCategory { get; set; }
}