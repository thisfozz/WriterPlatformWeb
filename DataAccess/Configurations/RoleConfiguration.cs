using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(e => e.RoleId).HasName("roles_pkey");

        builder.ToTable("roles");

        builder.HasIndex(e => e.Name, "roles_name_key").IsUnique();

        builder.Property(e => e.RoleId).HasColumnName("role_id");
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
    }
}