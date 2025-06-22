using Blogifier.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Entities
{
    public class ProductCategory : Entity
    {
        [Required, StringLength(80)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }

        public static ProductCategory Create(int categoryId, string name, string description = null)
        {
            var category = new ProductCategory
            {
                Id = categoryId,
                Name = name,
                Description = description
            };
            return category;
        }
    }
}
