using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sirindar.Core.Repositories
{
    public interface IRepository<TEntity,in TKey> 
        where TEntity : class
    {
        TEntity Get(TKey id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Update(TEntity entity);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void Remove(TKey id);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
