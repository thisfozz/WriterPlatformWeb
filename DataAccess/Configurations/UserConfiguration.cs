using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(e => e.UserId).HasName("users_pkey");

        builder.ToTable("users");

        builder.HasIndex(e => e.Email, "users_email_key").IsUnique();

        builder.HasIndex(e => e.Login, "users_login_key").IsUnique();

        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.Email)
            .HasMaxLength(100)
            .HasColumnName("email");
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnName("is_deleted");
        builder.Property(e => e.Login)
            .HasMaxLength(50)
            .HasColumnName("login");
        builder.Property(e => e.PasswordHash).HasColumnName("password_hash");
        builder.Property(e => e.RoleId).HasColumnName("role_id");

        builder.HasOne(d => d.Role).WithMany(p => p.Users)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("users_role_id_fkey");
    }
}