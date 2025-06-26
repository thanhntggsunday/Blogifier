using System;
using System.Collections.Generic;
using System.Data;
using Blogifier.Core.AdoNet.SQLServer;
using Blogifier.Core.Modules.Pms.Interfaces;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Blogifier.Core.Modules.Pms.Repositories;

namespace Blogifier.Core.Modules.Pms.Providers
{
    public class ProductProvider : IProvider<ProductDto>
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

        public ProductDto GetById(ProductDto item)
        {
            try
            {
                return DbContext.GetProductById(item);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public IEnumerable<ProductDto> GetAll()
        {
            try
            {
                var mapper = Mapper.CreateMapper<ProductDto>();
                return DbContext.GetAll("Select * From Product", CommandType.Text, mapper);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public IEnumerable<ProductDto> Find(Dictionary<string, object> condition)
        {
            try
            {
                var productName = condition["name"]?? String.Empty;
                return DbContext.FindProduct(productName.ToString());
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void Add(ProductDto item)
        {
            try
            {
                DbContext.AddProduct(item);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void AddRange(IEnumerable<ProductDto> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ProductDto item)
        {
            try
            {
                var cols = new List<string>();
                DbContext.UpdateProduct(item, cols);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void Remove(ProductDto item)
        {
            try
            {
                DbContext.RemoveProducts(item);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void RemoveRange(IEnumerable<ProductDto> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}