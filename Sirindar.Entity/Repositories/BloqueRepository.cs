using System.Data.Entity;
using System.Linq;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class BloqueRepository : Repository<Bloque>, IBloqueRepository
    {
        private SirindarDbContext sirindarDbContext
        {
            get { return Context as SirindarDbContext; }
        }

        public BloqueRepository(DbContext context) : base(context)
        {
        }

        public Bloque GetWithGrupos(int blqoueId)
        {
            return sirindarDbContext.Bloques
                 .Include(b => b.Grupos)
                 .First(b => b.EsActivo && b.BloqueId == blqoueId);
        }

        public bool IsBloque(int bloqueId)
        {
            return SingleOrDefault(b => b.BloqueId == bloqueId) != null;
        }

        public void SumaKilocalorias(int bloqueId)
        {
            var bloque = sirindarDbContext.Bloques.Include("Grupos").First(b => b.BloqueId == bloqueId);
            bloque.KilocaloriasTotales = 0;
            bloque.Grupos.Where(g => g.EsActivo).ToList().ForEach(g => bloque.KilocaloriasTotales += g.Kilocalorias);
        }
    }
}