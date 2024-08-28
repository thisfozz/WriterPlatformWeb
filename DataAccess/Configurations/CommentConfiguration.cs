using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasKey(e => e.CommentsId).HasName("comments_pkey");

        builder.ToTable("comments");

        builder.Property(e => e.CommentsId).HasColumnName("comments_id");
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("created_at");
        builder.Property(e => e.Text).HasColumnName("text");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.WorksId).HasColumnName("works_id");

        builder.HasOne(d => d.User).WithMany(p => p.Comments)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("comments_user_id_fkey");

        builder.HasOne(d => d.Works).WithMany(p => p.Comments)
            .HasForeignKey(d => d.WorksId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("comments_works_id_fkey");
    }
}