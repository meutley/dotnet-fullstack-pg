using SourceName.Service.Model.Users;

namespace SourceName.Service.Users
{
    public interface IUserCapabilitiesService
    {
        ApplicationUserCapabilities GetUserCapabilities(int userId);
    }
}