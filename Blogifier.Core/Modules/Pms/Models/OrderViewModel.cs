using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Modules.Pms.Models.Dto;

namespace Blogifier.Shared.Modules.Pms.Models
{
    public class OrderViewModel : BaseViewModel<OrderDto>
    {
        public OrderViewModel()
        {
            Data = new OrderDto();
        }
    }
}
