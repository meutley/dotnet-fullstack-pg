using System.Linq;

using AutoMapper;

using SourceName.Api.Model.Roles;
using SourceName.Api.Model.Users;

using SourceName.Service.Model.Roles;
using SourceName.Service.Model.Users;

namespace SourceName.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            // API -> Service
            CreateMap<AuthenticateUserRequest, ApplicationUser>();

            CreateMap<CreateUserRequest, ApplicationUser>()
                .ForMember(
                    destination => destination.Roles,
                    opt => opt.MapFrom(source =>
                        source.RoleIds.Select(
                            r => new ApplicationUserRole
                            {
                                ApplicationRoleId = r
                            })));

            CreateMap<UpdateUserRequest, ApplicationUser>()
                .ForMember(
                    destination => destination.Roles,
                    opt => opt.MapFrom(source =>
                        source.RoleIds.Select(
                            r => new ApplicationUserRole
                            {
                                ApplicationRoleId = r
                            })));

            // Service -> API
            CreateMap<ApplicationUser, ApplicationUserResource>();
            CreateMap<ApplicationUserCapabilities, ApplicationUserCapabilitiesResource>();
            CreateMap<ApplicationUserRole, ApplicationUserRoleResource>();
            CreateMap<ApplicationRole, ApplicationRoleResource>();
        }
    }
}