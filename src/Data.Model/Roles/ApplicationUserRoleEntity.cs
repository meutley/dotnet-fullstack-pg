using SourceName.Data.Model.Users;

namespace SourceName.Data.Model.Roles
{
    public class ApplicationUserRoleEntity : EntityWithIntegerId
    {
        public int ApplicationUserId { get; set; }
        public int ApplicationRoleId { get; set; }

        public virtual ApplicationUserEntity User { get; set; }
        public virtual ApplicationRoleEntity Role { get; set; }
    }
}