using Blogifier.Core.Entities.Base;

namespace Blogifier.Core.Entities
{
    public class ProductRelatedProduct : Entity
    {
        public int ProductId { get; set; }
        public int RelatedProductId { get; set; }
    }
}
