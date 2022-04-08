namespace CareerApplication.Core.Services;

public class MappingService : Profile
{
    public MappingService()
    {
        // Mapping Entity <---> Model
        CreateMap<SectorEntity, Sector>().ReverseMap();
        CreateMap<JobEntity, Job>().ReverseMap();
        CreateMap<RoleEntity, Role>().ReverseMap();
        CreateMap<UserEntity, User>().ReverseMap();

        // Mapping Firebase Object <---> Entity
        CreateMap<FirebaseObject<SectorEntity>, SectorEntity>().ReverseMap();
        CreateMap<FirebaseObject<JobEntity>, JobEntity>().ReverseMap();
        CreateMap<FirebaseObject<RoleEntity>, RoleEntity>().ReverseMap();
        CreateMap<FirebaseObject<UserEntity>, UserEntity>().ReverseMap();
    }
}
