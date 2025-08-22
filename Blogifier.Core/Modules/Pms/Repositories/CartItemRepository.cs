using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.AdoNet.SQLServer;
using Blogifier.Core.Modules.Pms.Extensions;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Microsoft.Data.SqlClient;

namespace Blogifier.Core.Modules.Pms.Repositories
{
    public static class CartItemRepository
    {
        public static CartItemDto GetCartItemById(this DataAccess dataAccess, CartItemDto dto)
        {
            var mapper = Mapper.CreateMapper<CartItemDto>();
            var cmd = dto.ToEntity().GenerateGetByIdCommand("CartItems");

            return dataAccess.GetById(cmd, mapper);
        }

        public static List<CartItemDto> FindCartItem(this DataAccess dataAccess, int CartId)
        {
            var mapper = Mapper.CreateMapper<CartItemDto>();
            var cmd = new SqlCommand(@"select * from CartItems where CartId = @CartId");

            cmd.Parameters.AddWithValue("@CartId", CartId);
            return dataAccess.Find(cmd, mapper);
        }

        public static void AddCartItem(this DataAccess dataAccess, CartItemDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateInsertCommand("CartItems");

            dataAccess.ExecuteScalar(cmd);
        }

        public static void UpdateCartItem(this DataAccess dataAccess, CartItemDto itemDto, List<string> cols)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateUpdateCommand("CartItems", cols);

            dataAccess.ExecuteScalar(cmd);
        }

        public static void RemoveCartItem(this DataAccess dataAccess, CartItemDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateDeleteCommand("CartItems");

            dataAccess.ExecuteScalar(cmd);
        }
    }
}
