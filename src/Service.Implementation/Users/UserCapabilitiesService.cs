using System.Collections.Generic;
using System.Linq;

using SourceName.Service.Model.Roles;
using SourceName.Service.Model.Users;
using SourceName.Service.Users;

namespace SourceName.Service.Implementation.Users
{
    public class UserCapabilitiesService : IUserCapabilitiesService
    {
        private static List<ApplicationRoles> RolesCanManageUsers = new List<ApplicationRoles>
        {
            ApplicationRoles.Administrator
        };
        
        private readonly IUserService _userService;

        public UserCapabilitiesService(
            IUserService userService
        )
        {
            _userService = userService;
        }
        
        public ApplicationUserCapabilities GetUserCapabilities(int userId)
        {
            var user = _userService.GetById(userId);
            return new ApplicationUserCapabilities
            {
                CanManageUsers = GetCanManageUsers(user)
            };
        }

        private bool GetCanManageUsers(ApplicationUser user)
        {
            return user.Roles.Any(
                r =>
                RolesCanManageUsers.Select(r => (int)r).Contains(r.ApplicationRoleId));
        }
    }
}