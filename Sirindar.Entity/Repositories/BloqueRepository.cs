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
    }
}