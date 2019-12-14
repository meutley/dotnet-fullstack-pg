using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SourceName.Data.Model.Roles;

namespace SourceName.Data.Model.EntityTypeConfigurations.Roles
{
    public class ApplicationUserRoleEntityConfiguration : IEntityTypeConfiguration<ApplicationUserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRoleEntity> builder)
        {
            builder.ToTable("application_user_role");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.ApplicationUserId)
                .HasColumnName("application_user_id")
                .IsRequired();

            builder.Property(x => x.ApplicationRoleId)
                .HasColumnName("application_role_id")
                .IsRequired();

            builder.HasIndex(x => new { x.ApplicationUserId, x.ApplicationRoleId })
                .IsUnique();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.ApplicationUserId);

            builder.HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.ApplicationRoleId);
        }
    }
}