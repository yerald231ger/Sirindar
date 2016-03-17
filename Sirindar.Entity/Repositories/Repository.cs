using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity, int> where TEntity : TableDbConventions
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, string include = "")
        {
            return DbSet.Include(include).Where(predicate).ToList().First();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().Where(e => e.EsActivo).ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(e => e.EsActivo).Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(e => e.EsActivo).SingleOrDefault(predicate);
        }

        public void Update(TEntity entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            entity.EsActivo = false;
        }

        public void Remove(int id)
        {
            Context.Set<TEntity>().Find(id).EsActivo = false;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.EsActivo = false;
            }
        }

        public int Count()
        {
            return Context.Set<TEntity>().Count();
        }
    }
}
