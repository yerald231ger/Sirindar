using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    class UsuarioRepository : IUsuarioRepository
    {
        public void Update(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Usuario entity) 
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Usuario> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> Find(Expression<Func<Usuario, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Usuario Get(string id)
        {
            throw new NotImplementedException();
        }

        public Usuario Get(Expression<Func<Usuario, bool>> predicate, string include)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Usuario> entities)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Usuario SingleOrDefault(Expression<Func<Usuario, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
