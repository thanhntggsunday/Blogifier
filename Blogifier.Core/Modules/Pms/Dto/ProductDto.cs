using System;
using System.Collections.Generic;
using System.Text;

namespace Blogifier.Core.Modules.Pms.Models.Dto
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public double Star { get; set; }

        // n-1 relationships
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
