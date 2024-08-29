using DataAccess.Configurations;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts;

public partial class WriterPlatformContext : DbContext
{
    public WriterPlatformContext(DbContextOptions<WriterPlatformContext> options)
        : base(options) 
    {

    }

    public virtual DbSet<AuthorEntity> Authors { get; set; }
    public virtual DbSet<CommentEntity> Comments { get; set; }
    public virtual DbSet<GenreEntity> Genres { get; set; }
    public virtual DbSet<RatingEntity> Ratings { get; set; }
    public virtual DbSet<RoleEntity> Roles { get; set; }
    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<WorkEntity> Works { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new RatingConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new WorkConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}