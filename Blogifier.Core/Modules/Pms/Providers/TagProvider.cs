using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Blogifier.Core.AdoNet.SQLServer;
using Blogifier.Core.Modules.Pms.Interfaces;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Blogifier.Core.Modules.Pms.Repositories;

namespace Blogifier.Core.Modules.Pms.Providers
{
    public class TagProvider : BaseProvider, IProvider<TagDto>
    {
        public TagDto GetById(TagDto item)
        {
            try
            {
                return DbContext.GetTagById(item);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public IEnumerable<TagDto> GetAll()
        {
            try
            {
                var mapper = Mapper.CreateMapper<TagDto>();
                return DbContext.GetAll("Select * From Tags", CommandType.Text, mapper);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public IEnumerable<TagDto> Find(Dictionary<string, object> condition)
        {
            try
            {
                var name = condition["name"] ?? String.Empty;
                return DbContext.FindTag(name.ToString());
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void Add(TagDto item)
        {
            try
            {
                DbContext.AddTag(item);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void AddRange(IEnumerable<TagDto> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(TagDto entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TagDto entity, List<string> cols)
        {
            try
            {
                DbContext.UpdateTag(entity, cols);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void Remove(TagDto item)
        {
            try
            {
                DbContext.RemoveTag(item);
            }
            finally
            {
                DbContext.Dispose();
            }
        }

        public void RemoveRange(IEnumerable<TagDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
