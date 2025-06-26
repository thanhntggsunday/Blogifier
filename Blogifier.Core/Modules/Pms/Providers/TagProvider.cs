using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Modules.Pms.Interfaces;
using Blogifier.Core.Modules.Pms.Models.Dto;

namespace Blogifier.Core.Modules.Pms.Providers
{
    public class TagProvider : IProvider<TagDto>
    {
        public TagDto GetById(TagDto item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TagDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TagDto> Find(Dictionary<string, object> condition)
        {
            throw new NotImplementedException();
        }

        public void Add(TagDto entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<TagDto> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(TagDto entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(TagDto entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<TagDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
