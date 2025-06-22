using System.Collections.Generic;
using System.Data;
using System.Linq;
using Blogifier.Core.AdoNet.SQLServer;
using Blogifier.Core.Modules.Pms.Extensions;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Microsoft.Data.SqlClient;

namespace Blogifier.Core.Modules.Pms.Repositories
{
    public static class ProductRepository
    {
        public static List<ProductDto> GetProducts(this DataAccess dataAccess, string cultureName = "")
        {
            var mapper = Mapper.CreateMapper<ProductDto>();
            return dataAccess.GetAllItems("Select * From Product", CommandType.Text, mapper);
        }

        public static void CreatProducts(this DataAccess dataAccess, ProductDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateInsertCommand("Product");

            dataAccess.ExecuteScalar(cmd);
        }
    }
}