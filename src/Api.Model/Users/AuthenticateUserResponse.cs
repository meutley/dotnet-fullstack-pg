namespace SourceName.Api.Model.Users
{
    public class AuthenticateUserResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
}