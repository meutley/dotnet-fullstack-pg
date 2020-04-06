using System.Collections.Generic;

namespace SourceName.Service.Users
{
    public interface IUserContextService
    {
        int? UserId { get; }

        void SetCurrentUserId(string userId);
    }
}
