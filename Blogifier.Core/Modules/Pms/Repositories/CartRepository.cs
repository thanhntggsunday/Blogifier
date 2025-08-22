using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.AdoNet.SQLServer;
using Blogifier.Core.Modules.Pms.Extensions;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Microsoft.Data.SqlClient;

namespace Blogifier.Core.Modules.Pms.Repositories
{
    public static class CartRepository
    {
        public static CartDto GetCartById(this DataAccess dataAccess, CartDto dto)
        {
            var mapper = Mapper.CreateMapper<CartDto>();
            var cmd = dto.ToEntity().GenerateGetByIdCommand("Carts");

            return dataAccess.GetById(cmd, mapper);
        }

        public static List<CartDto> FindCart(this DataAccess dataAccess, string UserName = "")
        {
            var mapper = Mapper.CreateMapper<CartDto>();
            var cmd = new SqlCommand(@"select * from Carts where [UserName] like @Name");

            var value = $"'%{UserName}%'";
            cmd.Parameters.AddWithValue("@Name", value);
            return dataAccess.Find(cmd, mapper);
        }

        public static int AddCart(this DataAccess dataAccess, CartDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateInsertCommand("Carts");

            var id = dataAccess.ExecuteScalar(cmd);

            return Int32.Parse(id.ToString());
        }

        public static void UpdateCart(this DataAccess dataAccess, CartDto itemDto, List<string> cols)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateUpdateCommand("Carts", cols);

            dataAccess.ExecuteScalar(cmd);
        }

        public static void RemoveCart(this DataAccess dataAccess, CartDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateDeleteCommand("Carts");

            dataAccess.ExecuteScalar(cmd);
        }
    }
}
