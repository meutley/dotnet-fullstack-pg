using Microsoft.EntityFrameworkCore;

using SourceName.Data.Model.Users;

namespace SourceName.Data.Model
{
    public class SourceNameContext : DbContext
    {
        public DbSet<ApplicationUserEntity> Users { get; set; }

        public SourceNameContext(DbContextOptions<SourceNameContext> options)
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SourceNameContext).Assembly);
        }
    }
}