using System.Collections.Generic;

namespace Blogifier.Core.Modules.Pms.Interfaces
{
    public interface IProvider<TEntity> where TEntity : class
    {
        TEntity GetById(TEntity item);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Dictionary<string, object> condition);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(TEntity entity, List<string> cols);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}