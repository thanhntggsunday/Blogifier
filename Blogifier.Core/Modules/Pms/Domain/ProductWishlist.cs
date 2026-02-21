using Blogifier.Core.Entities.Base;

namespace Blogifier.Core.Entities
{
    public class ProductWishlist : Entity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
