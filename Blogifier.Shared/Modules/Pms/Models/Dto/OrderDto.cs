using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Entities;

namespace Blogifier.Core.Modules.Pms.Models.Dto
{
    public class OrderDto : BaseDto
    {
        public string UserName { get; set; }
        //public Address BillingAddress { get; set; }
        //public Address ShippingAddress { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
        public decimal GrandTotal { get; set; }
        public int AddressId { get; set; }

        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
