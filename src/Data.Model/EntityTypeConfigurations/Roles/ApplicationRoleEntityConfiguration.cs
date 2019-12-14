using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SourceName.Data.Model.Roles;

namespace SourceName.Data.Model.EntityTypeConfigurations.Roles
{
    public class ApplicationRoleEntityConfiguration : IEntityTypeConfiguration<ApplicationRoleEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationRoleEntity> builder)
        {
            builder.ToTable("application_role");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}