using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SourceName.Data.Implementation.Users;
using SourceName.Data.Users;
using SourceName.Data.Model;

namespace SourceName.DependencyInjection
{
    public class DataModule : IDependencyInjectionModule
    {
        private readonly string _connectionString;

        public DataModule(string connectionString) => _connectionString = connectionString;
        
        public void RegisterDependencies(IServiceCollection services)
        {
            services.AddDbContext<SourceNameContext>(options => options.UseNpgsql(_connectionString));
            
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}