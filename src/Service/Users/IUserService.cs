using System.Collections.Generic;

using SourceName.Service.Model.Users;

namespace SourceName.Service.Users
{
    public interface IUserService
    {
        ApplicationUser CreateUser(ApplicationUser user);
        void DeleteUser(int id);
        List<ApplicationUser> GetAll();
        ApplicationUser GetById(int id);
        ApplicationUser GetByUsername(string username);
        ApplicationUser GetForAuthentication(string username);
        ApplicationUser UpdateUser(ApplicationUser user);
    }
}