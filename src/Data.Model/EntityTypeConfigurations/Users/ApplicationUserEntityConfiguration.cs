using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SourceName.Data.Model.Users;

namespace SourceName.Data.Model.EntityTypeConfigurations.Users
{
    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUserEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserEntity> builder)
        {
            builder.ToTable("application_user");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Username)
                .HasColumnName("username")
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(x => x.Username)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired();

            builder.Property(x => x.PasswordSalt)
                .HasColumnName("password_salt")
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasColumnName("is_active")
                .IsRequired();
        }
    }
}