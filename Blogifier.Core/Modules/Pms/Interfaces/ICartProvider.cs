using Blogifier.Core.Modules.Pms.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogifier.Core.Modules.Pms.Interfaces
{
    public interface ICartProvider : IProvider<CartDto>
    {
        IEnumerable<CartItemDto> FindCartItemByCartId(CartDto condition);
    }
}
