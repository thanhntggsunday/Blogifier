using Blogifier.Core.Data.ValueGeneration;
using Blogifier.Core.Identity;
using Blogifier.Core.Newsletters;
using Blogifier.Core.Options;
using Blogifier.Core.Posts;
using Blogifier.Core.Storages;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Data;

public class SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : AppDbContext(options)
{
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<UserInfo>(e =>
    {
      e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");

      // https://github.com/dotnet/EntityFramework.Docs/issues/3057
      // https://github.com/dotnet/efcore/issues/19765
      // TOTO No solution has been found
      // This configuration is not updated when the entity is updated
      e.Property(b => b.UpdatedAt)
        .HasValueGenerator(typeof(DateTimetValueGenerator));
    });
    modelBuilder.Entity<OptionInfo>(e =>
    {
      e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
      e.Property(b => b.UpdatedAt)
        .HasValueGenerator(typeof(DateTimetValueGenerator));
    });

    modelBuilder.Entity<Post>(e =>
    {
      e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
      e.Property(b => b.UpdatedAt)
        .HasValueGenerator(typeof(DateTimetValueGenerator));
    });

    modelBuilder.Entity<Category>(e =>
    {
      e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
    });

    modelBuilder.Entity<Newsletter>(e =>
    {
      e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
      e.Property(b => b.UpdatedAt)
        .HasValueGenerator(typeof(DateTimetValueGenerator));
    });

    modelBuilder.Entity<Subscriber>(e =>
    {
      e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
      e.Property(b => b.UpdatedAt)
        .HasValueGenerator(typeof(DateTimetValueGenerator));
    });

    modelBuilder.Entity<Storage>(e =>
    {
      e.Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");
    });

    //modelBuilder.Entity<StorageReference>(e =>
    //{
    //  e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
    //});
  }
}
