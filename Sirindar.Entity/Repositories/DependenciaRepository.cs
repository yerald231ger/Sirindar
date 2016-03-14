using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class DependenciaRepository : Repository<Dependencia>, IDependenciaRepository
    {
        private SirindarDbContext SirindarDbContext
        {
            get { return Context as SirindarDbContext; }
        }

        public DependenciaRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Dependencia> SearchByNombre(string dependencia)
        {
            return GetAll().Where(d => d.Nombre
                    .ToLowerInvariant()
                    .Contains(dependencia.ToLowerInvariant()))
                .OrderBy(d => d.Nombre).ToList();
        }
    }
}