using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class WorkConfiguration : IEntityTypeConfiguration<WorkEntity>
{
    public void Configure(EntityTypeBuilder<WorkEntity> builder)
    {
        builder.HasKey(e => e.WorksId).HasName("works_pkey");

        builder.ToTable("works");

        builder.Property(e => e.WorksId).HasColumnName("works_id");
        builder.Property(e => e.AuthorId).HasColumnName("author_id");
        builder.Property(e => e.AverageRating)
            .HasPrecision(5, 2)
            .HasDefaultValueSql("0")
            .HasColumnName("average_rating");
        builder.Property(e => e.GenreId).HasColumnName("genre_id");
        builder.Property(e => e.PublicationDate).HasColumnName("publication_date");
        builder.Property(e => e.Text).HasColumnName("text");
        builder.Property(e => e.Title)
            .HasMaxLength(255)
            .HasColumnName("title");

        builder.HasOne(d => d.Author).WithMany(p => p.Works)
            .HasForeignKey(d => d.AuthorId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("works_author_id_fkey");

        builder.HasOne(d => d.Genre).WithMany(p => p.Works)
            .HasForeignKey(d => d.GenreId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("works_genre_id_fkey");
    }
}