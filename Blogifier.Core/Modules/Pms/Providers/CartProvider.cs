using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Modules.Pms.Interfaces;
using Blogifier.Core.Modules.Pms.Models.Dto;

namespace Blogifier.Core.Modules.Pms.Providers
{
    public class CartProvider : BaseProvider, IProvider<CartDto>
    {
        public CartDto GetById(CartDto item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartDto> Find(Dictionary<string, object> condition)
        {
            throw new NotImplementedException();
        }

        public void Add(CartDto entity)
        {
            throw new NotImplementedException();
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
    }
}
