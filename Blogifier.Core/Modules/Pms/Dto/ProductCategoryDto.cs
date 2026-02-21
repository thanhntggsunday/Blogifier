using System;
using System.Collections.Generic;
using System.Text;

namespace Blogifier.Core.Modules.Pms.Models.Dto
{
    public class ProductCategoryDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
    }
}
