using AspnetRun.Core.Entities;
using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Data
{
	public class BlogifierDbContext : IdentityDbContext<ApplicationUser>
    {
        public BlogifierDbContext(DbContextOptions<BlogifierDbContext> options) : base(options) { }

        #region DB Sets

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<CustomField> CustomFields { get; set; }

        #endregion


        #region Pms
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Compare> Compares { get; set; }
        public DbSet<ProductCompare> ProductCompares { get; set; }
        public DbSet<ProductRelatedProduct> ProductRelatedProducts { get; set; }
        public DbSet<ProductWishlist> ProductWishlists { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ApplicationSettings.DatabaseOptions(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
