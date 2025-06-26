using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Modules.Pms.Interfaces;
using Blogifier.Core.Modules.Pms.Models.Dto;

namespace Blogifier.Core.Modules.Pms.Providers
{
    public class OrderProvider : BaseProvider, IProvider<OrderDto>
    {
        public OrderDto GetById(OrderDto item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDto> Find(Dictionary<string, object> condition)
        {
            throw new NotImplementedException();
        }

        public void Add(OrderDto entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<OrderDto> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDto entity)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDto entity, List<string> cols)
        {
            throw new NotImplementedException();
        }

        public void Remove(OrderDto entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<OrderDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
