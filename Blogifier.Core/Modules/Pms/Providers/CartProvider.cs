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
    public class CartProvider : BaseProvider, ICartProvider
    {
        public CartDto GetById(CartDto item)
        {
            try
            {
                var cart = DbContext.GetCartById(item);
                var cartItems = DbContext.FindCartItem(cart.Id);

                cart.Items.AddRange(cartItems);

                return cart;
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public IEnumerable<CartDto> GetAll()
        {
            try
            {
                var mapper = Mapper.CreateMapper<CartDto>();
                var carts = DbContext.GetAll("Select * From Carts", CommandType.Text, mapper);
                
                return carts;
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public IEnumerable<CartDto> Find(Dictionary<string, object> condition)
        {
            try
            {
                var name = condition["name"] ?? String.Empty;
                return DbContext.FindCart(name.ToString());
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void Add(CartDto item)
        {
            try
            {
                var cartId = DbContext.AddCart(item);

                item.SetCartId(cartId);

                for (int i = 0; i < item.Items.Count; i++)
                {
                    var cartItem = item.Items[i];

                    DbContext.AddCartItem(cartItem);
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

        public void AddRange(IEnumerable<CartDto> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(CartDto entity)
        {
            throw new NotImplementedException();
        }

        public void Update(CartDto entity, List<string> cols)
        {
            throw new NotImplementedException();
        }

        public void Remove(CartDto entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<CartDto> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartItemDto> FindCartItemByCartId(CartDto condition)
        {
            throw new NotImplementedException();
        }
    }
}
