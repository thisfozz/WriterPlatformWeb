using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
{
    public void Configure(EntityTypeBuilder<AuthorEntity> builder)
    {
        builder.HasKey(e => e.AuthorId).HasName("authors_pkey");

        builder.ToTable("authors");

        builder.Property(e => e.AuthorId).HasColumnName("author_id");
        builder.Property(e => e.FirstName)
            .HasMaxLength(50)
            .HasColumnName("first_name");
        builder.Property(e => e.LastName)
            .HasMaxLength(50)
            .HasColumnName("last_name");
    }
}