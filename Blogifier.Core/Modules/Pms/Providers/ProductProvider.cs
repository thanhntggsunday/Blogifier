using Blogifier.Core.AdoNet;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Blogifier.Core.Modules.Pms.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogifier.Core.Modules.Pms.Providers
{
    public class ProductProvider
    {
        private readonly Blogifier.Core.AdoNet.SQLServer.DataAccess _dbContext;

        public ProductProvider()
        {
            _dbContext = new Blogifier.Core.AdoNet.SQLServer.DataAccess();
        }

        public List<ProductDto> GetProducts()
        {
            try
            {
                return _dbContext.GetProducts();
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
               
            }
            finally
            {
                _dbContext.Dispose();
            }
        }
    }
}
