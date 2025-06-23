using System.Collections.Generic;
using Blogifier.Core.AdoNet.SQLServer;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Blogifier.Core.Modules.Pms.Repositories;

namespace Blogifier.Core.Modules.Pms.Providers
{
    public class ProductProvider
    {
        private readonly DataAccess _dbContext;

        public ProductProvider()
        {
            _dbContext = new DataAccess();
        }

        public List<ProductDto> GetProducts(string productName)
        {
            try
            {
                return _dbContext.GetProducts(productName);
            }
            finally
            {
                _dbContext.Dispose();
            }
        }

        public void CreatProducts(ProductDto item)
        {
            try
            {
                _dbContext.CreatProducts(item);
            }
            finally
            {
                _dbContext.Dispose();
            }
        }

        public void UpdateProducts(ProductDto item)
        {
            try
            {
                var cols = new List<string>();
                _dbContext.UpdateProducts(item, cols);
            }
            finally
            {
                _dbContext.Dispose();
            }
        }

        public void DeleteProduct(ProductDto item)
        {
            try
            {
                _dbContext.DeleteProducts(item);
            }
            finally
            {
                _dbContext.Dispose();
            }
        }
    }
}