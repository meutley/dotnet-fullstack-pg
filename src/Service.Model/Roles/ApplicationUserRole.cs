namespace SourceName.Service.Model.Roles
{
    public class ApplicationUserRole
    {
        int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public int ApplicationRoleId { get; set; }
        public ApplicationRole Role { get; set; }
    }
}