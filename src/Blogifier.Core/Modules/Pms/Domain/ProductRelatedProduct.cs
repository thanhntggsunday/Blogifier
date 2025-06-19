using AspnetRun.Core.Entities.Base;

namespace AspnetRun.Core.Entities
{
    public class ProductRelatedProduct : Entity
    {
        public int ProductId { get; set; }
        public int RelatedProductId { get; set; }
    }
}
