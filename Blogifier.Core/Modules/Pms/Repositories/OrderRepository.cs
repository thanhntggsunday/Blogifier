using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.AdoNet.SQLServer;
using Blogifier.Core.Modules.Pms.Extensions;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Microsoft.Data.SqlClient;

namespace Blogifier.Core.Modules.Pms.Repositories
{
    public static class OrderRepository
    {
        public static OrderDto GetOrderById(this DataAccess dataAccess, OrderDto dto)
        {
            var mapper = Mapper.CreateMapper<OrderDto>();
            var cmd = dto.ToEntity().GenerateGetByIdCommand("Orders");

            return dataAccess.GetById(cmd, mapper);
        }

        public static List<OrderDto> FindOrder(this DataAccess dataAccess, string UserName = "")
        {
            var mapper = Mapper.CreateMapper<OrderDto>();
            var cmd = new SqlCommand(@"select * from Orders where [UserName] like @Name");

            var value = $"'%{UserName}%'";
            cmd.Parameters.AddWithValue("@Name", value);
            return dataAccess.Find(cmd, mapper);
        }

        public static void AddOrder(this DataAccess dataAccess, OrderDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateInsertCommand("Orders");

            dataAccess.ExecuteScalar(cmd);
        }

        public static void UpdateOrder(this DataAccess dataAccess, OrderDto itemDto, List<string> cols)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateUpdateCommand("Orders", cols);

            dataAccess.ExecuteScalar(cmd);
        }

        public static void RemoveOrder(this DataAccess dataAccess, OrderDto itemDto)
        {
            var product = itemDto.ToEntity();
            var cmd = product.GenerateDeleteCommand("Orders");

            dataAccess.ExecuteScalar(cmd);
        }
    }
}
