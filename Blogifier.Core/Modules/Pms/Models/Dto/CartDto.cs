using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Entities;

namespace Blogifier.Core.Modules.Pms.Models.Dto
{
    public class CartDto
    {
        public string UserName { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
