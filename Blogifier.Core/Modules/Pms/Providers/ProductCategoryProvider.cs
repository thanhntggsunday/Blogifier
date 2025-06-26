using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Modules.Pms.Interfaces;
using Blogifier.Core.Modules.Pms.Models.Dto;

namespace Blogifier.Core.Modules.Pms.Providers
{
    public class ProductCategoryProvider : IProvider<ProductCategoryDto>
    {
        public ProductCategoryDto GetById(ProductCategoryDto item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductCategoryDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductCategoryDto> Find(Dictionary<string, object> condition)
        {
            throw new NotImplementedException();
        }

        public void Add(ProductCategoryDto entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<ProductCategoryDto> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductCategoryDto entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ProductCategoryDto entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<ProductCategoryDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
