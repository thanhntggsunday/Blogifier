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
        public static List<ProductDto> GetProducts(this DataAccess dataAccess, string productName = "")
        {
            var mapper = Mapper.CreateMapper<ProductDto>();
            var cmd = new SqlCommand("Select * From Product where [Name] like @Name");
            var value = $"'%{productName}%'";
            cmd.Parameters.AddWithValue("@Name", value);
            return dataAccess.GetItems(cmd, mapper);
        }

        public static void CreatProducts(this DataAccess dataAccess, ProductDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateInsertCommand("Product");

            dataAccess.ExecuteScalar(cmd);
        }

        public static void UpdateProducts(this DataAccess dataAccess, ProductDto itemDto, List<string> cols)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateUpdateCommand("Product", cols);

            dataAccess.ExecuteScalar(cmd);
        }

        public static void DeleteProducts(this DataAccess dataAccess, ProductDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateDeleteCommand("Product");

            dataAccess.ExecuteScalar(cmd);
        }
    }
}