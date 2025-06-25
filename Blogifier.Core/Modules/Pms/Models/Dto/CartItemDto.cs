using System;
using System.Collections.Generic;
using System.Text;

namespace Blogifier.Core.Modules.Pms.Models.Dto
{
    public class CartItemDto : BaseDto
    {
        public CartItemDto()
        {
            Product = new ProductDto();
            Cart = new CartDto();
        }

        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public ProductDto Product { get; set; }
        public CartDto Cart { get; set; }
    }
}
