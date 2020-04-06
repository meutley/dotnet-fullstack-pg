using System.Collections.Generic;
using System.Linq;

using SourceName.Service.Users;

namespace SourceName.Service.Implementation.Users
{
    public class UserContextService : IUserContextService
    {
        private int? _userId;

        public int? UserId
        {
            get
            {
                return _userId;
            }
        }

        public void SetCurrentUserId(string userId)
        {
            _userId = !string.IsNullOrWhiteSpace(userId)
                        ? int.Parse(userId)
                        : (int?)null;
        }
    }
}
