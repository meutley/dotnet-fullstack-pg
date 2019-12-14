using System.Collections.Generic;

using SourceName.Data.Model.Users;

namespace SourceName.Data.Users
{
    public interface IUserRepository : IRepository<ApplicationUserEntity>, IIntegerRepository<ApplicationUserEntity>
    {
        ApplicationUserEntity GetByUsernameWithRoles(string username);
    }
}