using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Blogifier.Core.AdoNet.SQLServer;
using Blogifier.Core.Common;
using Blogifier.Core.Modules.Pms.Interfaces;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Blogifier.Core.Modules.Pms.Repositories;

namespace Blogifier.Core.Modules.Pms.Providers
{
    public class OrderProvider : BaseProvider, IOrderProvider
    {
        public OrderDto GetById(OrderDto item)
        {
            try
            {
                var order = DbContext.GetOrderById(item);
                var cartItems = DbContext.FindOrderItem(order.Id);

                order.Items.AddRange(cartItems);

                return order;
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public IEnumerable<OrderDto> GetAll()
        {
            try
            {
                var mapper = Mapper.CreateMapper<OrderDto>();
                var orders = DbContext.GetAll("Select * From Orders", CommandType.Text, mapper);

                return orders;
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public IEnumerable<OrderDto> Find(Dictionary<string, object> condition)
        {
            try
            {
                var name = condition["UserName"] ?? String.Empty;
                return DbContext.FindOrder(name.ToString());
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void Add(OrderDto item)
        {
            try
            {
                DbContext.BeginTransaction();

                var orderId = DbContext.AddOrder(item);

                item.SetOrderId(orderId);

                for (int i = 0; i < item.Items.Count; i++)
                {
                    var orderItem = item.Items[i];

                    DbContext.AddOrderItem(orderItem);
                }

                DbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                DbContext.RollbackTransaction();
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void AddRange(IEnumerable<OrderDto> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDto dto)
        {
            try
            {
                for (int i = 0; i < dto.Items.Count; i++)
                {
                    var orderItem = dto.Items[i];
                    var cols = new List<string>() {
                        "Quantity",
                        "TotalPrice",
                        "UnitPrice",
                        "ModifiedBy",
                        "ModifiedBy",
                        "ModifiedDate"
                    };

                    DbContext.UpdateOrderItem(orderItem, cols);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                DbContext.RollbackTransaction();
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void Update(OrderDto entity, List<string> cols)
        {
            throw new NotImplementedException();
        }

        public void Remove(OrderDto entity)
        {

            try
            {
                DbContext.BeginTransaction();

                for (int i = 0; i < entity.Items.Count; i++)
                {
                    var orderItem = entity.Items[i];

                    DbContext.RemoveOrderItem(orderItem);
                }

                DbContext.RemoveOrder(entity);

                DbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                DbContext.RollbackTransaction();
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void RemoveRange(IEnumerable<OrderDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
