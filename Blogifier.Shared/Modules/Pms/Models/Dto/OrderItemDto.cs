using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Entities;

namespace Blogifier.Core.Modules.Pms.Models.Dto
{
    public class OrderItemDto : BaseDto
    {
        public OrderItemDto()
        {
            Product = new ProductDto();
            Order = new OrderDto();
        }

        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public ProductDto Product { get; set; }
        public OrderDto Order { get; set; }
    }
}
