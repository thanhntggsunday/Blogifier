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
        private readonly Blogifier.Core.AdoNet.SQLite.DataAccess _dbContext;

        public ProductProvider()
        {
            _dbContext = new Blogifier.Core.AdoNet.SQLite.DataAccess();
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
    }
}
