using System.Collections.Generic;
using Blogifier.Core.AdoNet.SQLServer;
using Blogifier.Core.Modules.Pms.Extensions;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Microsoft.Data.SqlClient;

namespace Blogifier.Core.Modules.Pms.Repositories
{
    public static class OrderItemRepository
    {
        public static OrderItemDto GetOrderItemById(this DataAccess dataAccess, OrderItemDto dto)
        {
            var mapper = Mapper.CreateMapper<OrderItemDto>();
            var cmd = dto.ToEntity().GenerateGetByIdCommand("OrderItems");

            return dataAccess.GetById(cmd, mapper);
        }

        public static List<OrderItemDto> FindOrderItem(this DataAccess dataAccess, int OrderId)
        {
            var mapper = Mapper.CreateMapper<OrderItemDto>();
            var cmd = new SqlCommand(@"select * from OrderItems where OrderId = @OrderId");

            cmd.Parameters.AddWithValue("@OrderId", OrderId);
            return dataAccess.Find(cmd, mapper);
        }

        public static void AddOrderItem(this DataAccess dataAccess, OrderItemDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateInsertCommand("OrderItems");

            dataAccess.ExecuteScalar(cmd);
        }

        public static void UpdateOrderItem(this DataAccess dataAccess, OrderItemDto itemDto,
            List<string> cols)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateUpdateCommand("OrderItems", cols);

            dataAccess.ExecuteScalar(cmd);
        }

        public static void RemoveOrderItem(this DataAccess dataAccess, OrderItemDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateDeleteCommand("OrderItems");

            dataAccess.ExecuteScalar(cmd);
        }
    }
}