using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<RatingEntity>
{
    public void Configure(EntityTypeBuilder<RatingEntity> builder)
    {
        builder.HasKey(e => e.RatingId).HasName("ratings_pkey");

        builder.ToTable("ratings");

        builder.Property(e => e.RatingId).HasColumnName("rating_id");
        builder.Property(e => e.RatingValue).HasColumnName("rating_value");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.WorksId).HasColumnName("works_id");

        builder.HasOne(d => d.User).WithMany(p => p.Ratings)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("ratings_user_id_fkey");

        builder.HasOne(d => d.Works).WithMany(p => p.Ratings)
            .HasForeignKey(d => d.WorksId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("ratings_works_id_fkey");
    }
}