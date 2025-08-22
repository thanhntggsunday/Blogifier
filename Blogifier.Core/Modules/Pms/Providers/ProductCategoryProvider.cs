using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Blogifier.Core.AdoNet.SQLServer;
using Blogifier.Core.Modules.Pms.Interfaces;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Blogifier.Core.Modules.Pms.Repositories;

namespace Blogifier.Core.Modules.Pms.Providers
{
    public class ProductCategoryProvider : BaseProvider, IProvider<ProductCategoryDto>
    {
        public ProductCategoryDto GetById(ProductCategoryDto item)
        {
            try
            {
                return DbContext.GetProductCategoryById(item);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public IEnumerable<ProductCategoryDto> GetAll()
        {
            try
            {
                var mapper = Mapper.CreateMapper<ProductCategoryDto>();
                return DbContext.GetAll("Select * From ProductCategory", CommandType.Text, mapper);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public IEnumerable<ProductCategoryDto> Find(Dictionary<string, object> condition)
        {
            try
            {
                var productName = condition["name"] ?? String.Empty;
                return DbContext.FindProductCategory(productName.ToString());
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void Add(ProductCategoryDto item)
        {
            try
            {
                DbContext.AddProductCategory(item);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void Update(ProductCategoryDto entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductCategoryDto item, List<string> cols)
        {
            try
            {
                DbContext.UpdateProductCategory(item, cols);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void Remove(ProductCategoryDto item)
        {
            try
            {
                DbContext.RemoveProductCategory(item);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void AddRange(IEnumerable<ProductCategoryDto> item)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<ProductCategoryDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
