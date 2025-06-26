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
        public static ProductDto GetProductById(this DataAccess dataAccess, ProductDto dto)
        {
            var mapper = Mapper.CreateMapper<ProductDto>();
            var cmd = dto.ToEntity().GenerateGetByIdCommand("Product");

            return dataAccess.GetById(cmd, mapper);
        }

        public static List<ProductDto> FindProduct(this DataAccess dataAccess, string productName = "")
        {
            var mapper = Mapper.CreateMapper<ProductDto>();
            var cmd = new SqlCommand(@"select pc.[Name], p.* from Product p
join ProductCategories pc on P.CategoryId = pc.Id
where [Name] like @Name");

            var value = $"'%{productName}%'";
            cmd.Parameters.AddWithValue("@Name", value);
            return dataAccess.Find(cmd, mapper);
        }

        public static void AddProduct(this DataAccess dataAccess, ProductDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateInsertCommand("Product");

            dataAccess.ExecuteScalar(cmd);
        }

        public static void UpdateProduct(this DataAccess dataAccess, ProductDto itemDto, List<string> cols)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateUpdateCommand("Product", cols);

            dataAccess.ExecuteScalar(cmd);
        }

        public static void RemoveProducts(this DataAccess dataAccess, ProductDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateDeleteCommand("Product");

            dataAccess.ExecuteScalar(cmd);
        }
    }
}