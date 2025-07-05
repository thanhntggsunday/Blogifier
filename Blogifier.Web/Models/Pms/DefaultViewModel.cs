using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Blogifier.Shared.Models;

namespace Blogifier.Web.Models.Pms
{
    public class DefaultViewModel : TransactionalInformation
    {
        public List<ProductDto> Products { get; set; }
    }
}
