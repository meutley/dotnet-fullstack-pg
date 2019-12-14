using System.Collections.Generic;

using SourceName.Data.Model.Roles;

namespace SourceName.Data.Model.Users
{
    public class ApplicationUserEntity : EntityWithIntegerId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ApplicationUserRoleEntity> Roles { get; set; }
    }
}