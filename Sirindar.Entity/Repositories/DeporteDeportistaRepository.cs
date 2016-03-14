using System.Data.Entity;
using System.Linq;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class DeporteDeportistaRepository : Repository<DeporteDeportista>, IDeporteDeportistaRepository
    {
        private SirindarDbContext sirindarDbContext
        {
            get { return Context as SirindarDbContext; }
        }

        public DeporteDeportistaRepository(DbContext context) : base(context)
        {
        }


        public DeporteDeportista FindByDeporteId(int deporteId)
        {
            return sirindarDbContext.DeportesDeportistas
                .Include(dd => dd.Deporte)
                .Where(dd => dd.EsActivo).First(dd => dd.DeporteId == deporteId);
        }
    }
}
