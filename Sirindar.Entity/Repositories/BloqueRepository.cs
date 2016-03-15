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
            var bloque = sirindarDbContext.Bloques.Include(b => b.Grupos).First(b => b.EsActivo && b.BloqueId == blqoueId);
            bloque.Grupos = bloque.Grupos.Where(g => g.EsActivo).ToList();
            return bloque;
        }

        public bool IsBloque(int bloqueId)
        {
            return SingleOrDefault(b => b.BloqueId == bloqueId) != null;
        }

        public void CalculateKilocalorias(int bloqueId)
        {
            var bloque = Get(b => b.BloqueId == bloqueId, "Grupos");
            bloque.KilocaloriasTotales = 0;
            bloque.Grupos.Where(g => g.EsActivo).ToList().ForEach(g => bloque.KilocaloriasTotales += g.Kilocalorias);
        }

    }
}