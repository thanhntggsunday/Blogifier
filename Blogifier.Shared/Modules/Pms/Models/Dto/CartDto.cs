using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blogifier.Core.Entities;

namespace Blogifier.Core.Modules.Pms.Models.Dto
{
    public class CartDto : BaseDto
    {
        public CartDto()
        {
            Items = new List<CartItemDto>();
        }

        public string UserName { get; set; }
        public List<CartItemDto> Items { get; set; }

        public void SetCartId(int cartId)
        {
            for (int i = 0; i < Items.Count(); i++)
            {
                var item = Items[i];

                item.CartId = cartId;
            }
        }
    }
}
