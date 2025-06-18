using Blogifier.Core.Identity;
using Blogifier.Core.Newsletters;
using Blogifier.Core.Options;
using Blogifier.Core.Posts;
using Blogifier.Core.Storages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Data;

public class AppDbContext(DbContextOptions options)
    : IdentityDbContext<UserInfo, IdentityRole<int>, int>(options)
{
  public DbSet<OptionInfo> Options { get; set; } = default!;
  public DbSet<Post> Posts { get; set; } = default!;
  public DbSet<Category> Categories { get; set; } = default!;
  public DbSet<PostCategory> PostCategories { get; set; } = default!;
  public DbSet<Newsletter> Newsletters { get; set; } = default!;
  public DbSet<Subscriber> Subscribers { get; set; } = default!;
  public DbSet<Storage> Storages { get; set; } = default!;
  //public DbSet<StorageReference> StorageReferences { get; set; } = default!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<UserInfo>(e =>
    {
      e.ToTable("Users");
      e.Property(p => p.Id).HasMaxLength(128);
      e.Property(p => p.CreatedAt).HasColumnOrder(0);
      e.Property(p => p.UpdatedAt).HasColumnOrder(1);
      e.Property(p => p.PasswordHash).HasMaxLength(256);
      e.Property(p => p.SecurityStamp).HasMaxLength(32);
      e.Property(p => p.ConcurrencyStamp).HasMaxLength(64);
      e.Property(p => p.PhoneNumber).HasMaxLength(32);
    });

    modelBuilder.Entity<IdentityRole<int>>(e =>
    {
      e.ToTable("Roles");
      e.Property(p => p.Name).HasMaxLength(128);
    });

    modelBuilder.Entity<IdentityUserRole<int>>(e =>
    {
      e.ToTable("UserRoles");
    });

    modelBuilder.Entity<IdentityUserClaim<int>>(e =>
    {
      e.ToTable("UserClaims");
      e.Property(p => p.ClaimType).HasMaxLength(16);
      e.Property(p => p.ClaimValue).HasMaxLength(256);
    });

    modelBuilder.Entity<IdentityUserLogin<int>>(e =>
    {
      e.ToTable("UserLogins");
      e.Property(p => p.ProviderDisplayName).HasMaxLength(128);
    });

    modelBuilder.Entity<IdentityUserToken<int>>(e =>
    {
      e.ToTable("UserTokens");
      e.Property(p => p.Value).HasMaxLength(1024);
    });

    modelBuilder.Entity<IdentityRoleClaim<int>>(e =>
    {
      e.ToTable("RoleClaims");
    });

    modelBuilder.Entity<OptionInfo>(e =>
    {
      e.ToTable("Options");
      e.HasIndex(b => b.Key).IsUnique();
    });

    modelBuilder.Entity<Post>(e =>
    {
      e.ToTable("Posts");
      e.HasIndex(b => b.Slug).IsUnique();
    });

    modelBuilder.Entity<Storage>(e =>
    {
      e.ToTable("Storages");
    });

    modelBuilder.Entity<PostCategory>(e =>
    {
      e.ToTable("PostCategories");
      e.HasKey(t => new { t.PostId, t.CategoryId });
    });

    // Optional: nếu dùng StorageReference
    //modelBuilder.Entity<StorageReference>(e =>
    //{
    //    e.ToTable("StorageReferences");
    //    e.HasKey(t => new { t.StorageId, t.EntityId });
    //});
  }
}
