using System.Collections.Generic;
using Blogifier.Core.AdoNet.SQLServer;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Blogifier.Core.Modules.Pms.Repositories;

namespace Blogifier.Core.Modules.Pms.Providers
{
    public class ProductProvider
    {
        private DataAccess _dbContext;

        public ProductProvider()
        {
            _dbContext = new DataAccess();
        }

        public DataAccess DbContext
        {
            get
            {
                if (_dbContext == null || _dbContext.Disposed == true)
                {
                    _dbContext = new DataAccess();
                }

                return _dbContext;
            }
        }

        public List<ProductDto> GetProducts(string productName)
        {
            try
            {
                return DbContext.GetProducts(productName);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void CreatProducts(ProductDto item)
        {
            try
            {
                DbContext.CreatProducts(item);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void UpdateProducts(ProductDto item)
        {
            try
            {
                var cols = new List<string>();
                DbContext.UpdateProducts(item, cols);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void DeleteProduct(ProductDto item)
        {
            try
            {
                DbContext.DeleteProducts(item);
            }
            finally
            {
                DbContext.Dispose();
            }
        }
    }
}