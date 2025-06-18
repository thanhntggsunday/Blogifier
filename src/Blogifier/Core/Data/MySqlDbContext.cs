using Blogifier.Core.Identity;
using Blogifier.Core.Newsletters;
using Blogifier.Core.Options;
using Blogifier.Core.Posts;
using Blogifier.Core.Storages;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Data;

public class MySqlDbContext : AppDbContext
{
  public MySqlDbContext(DbContextOptions options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);


    modelBuilder.Entity<UserInfo>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });
    modelBuilder.Entity<OptionInfo>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });

    modelBuilder.Entity<Post>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });

    modelBuilder.Entity<Category>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<Newsletter>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });

    modelBuilder.Entity<Subscriber>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });

    modelBuilder.Entity<Storage>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
    });

    //modelBuilder.Entity<StorageReference>(e =>
    //{
    //  e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
    //});
  }
}
