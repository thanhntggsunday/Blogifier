using System.Collections.Generic;
using Blogifier.Core.AdoNet.SQLServer;
using Blogifier.Core.Modules.Pms.Extensions;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Microsoft.Data.SqlClient;

namespace Blogifier.Core.Modules.Pms.Repositories
{
    public static class PorductCategoryRepository
    {
        public static ProductCategoryDto GetProductCategoryById(this DataAccess dataAccess, ProductCategoryDto dto)
        {
            var mapper = Mapper.CreateMapper<ProductCategoryDto>();
            var cmd = dto.ToEntity().GenerateGetByIdCommand("ProductCategory");

            return dataAccess.GetById(cmd, mapper);
        }

        public static List<ProductCategoryDto> FindProductCategory(this DataAccess dataAccess, string productName = "")
        {
            var mapper = Mapper.CreateMapper<ProductCategoryDto>();
            var cmd = new SqlCommand(@"select * from ProductCategory where [Name] like @Name");

            var value = $"'%{productName}%'";
            cmd.Parameters.AddWithValue("@Name", value);
            return dataAccess.Find(cmd, mapper);
        }

        public static void AddProductCategory(this DataAccess dataAccess, ProductCategoryDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateInsertCommand("ProductCategory");

            dataAccess.ExecuteScalar(cmd);
        }

        public static void UpdateProductCategory(this DataAccess dataAccess, ProductCategoryDto itemDto, List<string> cols)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateUpdateCommand("ProductCategory", cols);

            dataAccess.ExecuteScalar(cmd);
        }

        public static void RemoveProductCategory(this DataAccess dataAccess, ProductCategoryDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateDeleteCommand("ProductCategory");

            dataAccess.ExecuteScalar(cmd);
        }
    }
}