using System.Collections.Generic;

using SourceName.Api.Model.Roles;

namespace SourceName.Api.Model.Users
{
    public class ApplicationUserResource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public List<ApplicationUserRoleResource> Roles { get; set; }
        public bool IsActive { get; set; }
    }
}