using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blogifier.Core.Entities;

namespace Blogifier.Core.Modules.Pms.Models.Dto
{
    public class OrderDto : BaseDto
    {
        public OrderDto()
        {
            Items = new List<OrderItemDto>();
        }

        public string UserName { get; set; }
        //public Address BillingAddress { get; set; }
        //public Address ShippingAddress { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
        public decimal GrandTotal { get; set; }
        public int AddressId { get; set; }

        public List<OrderItemDto> Items { get; set; }

        public void SetOrderId(int cartId)
        {
            for (int i = 0; i < Items.Count(); i++)
            {
                var item = Items[i];

                item.OrderId = cartId;
            }
        }
    }
}
