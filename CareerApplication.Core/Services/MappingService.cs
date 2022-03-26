namespace CareerApplication.Core.Services;

public class MappingService : Profile
{
    public MappingService()
    {
        // Mapping Entity <---> Model
        CreateMap<JobCategoryEntity, JobCategoryModel>().ReverseMap();
        CreateMap<JobEntity, JobModel>().ReverseMap();
        CreateMap<RoleEntity, RoleModel>().ReverseMap();
        CreateMap<UserEntity, UserModel>().ReverseMap();

        // Mapping Firebase Object <---> Entity
        CreateMap<FirebaseObject<JobCategoryEntity>, JobCategoryEntity>().ReverseMap();
        CreateMap<FirebaseObject<JobEntity>, JobEntity>().ReverseMap();
        CreateMap<FirebaseObject<RoleEntity>, RoleEntity>().ReverseMap();
        CreateMap<FirebaseObject<UserEntity>, UserEntity>().ReverseMap();
    }
}
