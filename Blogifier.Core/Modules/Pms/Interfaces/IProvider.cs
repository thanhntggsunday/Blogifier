using System;
using System.Collections.Generic;
using System.Text;

namespace Blogifier.Core.Modules.Pms.Interfaces
{
    public interface IProvider<TEntity> where TEntity : class
    {
        TEntity GetById(TEntity item);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Dictionary<string, Object> condition);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
