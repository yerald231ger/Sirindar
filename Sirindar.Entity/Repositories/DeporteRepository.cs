using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class DeporteRepository : Repository<Deporte>, IDeporteRepository
    {
        private SirindarDbContext SirindarDbContext
        {
            get { return Context as SirindarDbContext; }
        }

        public DeporteRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Deporte> SearchByNombre(string deporte)
        {
            return GetAll().Where(d => d.Nombre
                        .ToLowerInvariant()
                        .Contains(deporte.ToLowerInvariant())
                    ).ToList();
        }
    }
}