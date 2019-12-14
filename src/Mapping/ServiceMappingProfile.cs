using AutoMapper;

using SourceName.Data.Model.Roles;
using SourceName.Data.Model.Users;

using SourceName.Service.Model.Roles;
using SourceName.Service.Model.Users;

namespace SourceName.Mapping
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            // Service -> Data

            // Data -> Service
          
            // Two way mappings
            CreateMap<ApplicationRoleEntity, ApplicationRole>().ReverseMap();
            CreateMap<ApplicationUserEntity, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUserRoleEntity, ApplicationUserRole>().ReverseMap();
        }
    }
}