using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Entities;

namespace Blogifier.Core.Modules.Pms.Models.Dto
{
    public class CartDto : BaseDto
    {
        public string UserName { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }
}
