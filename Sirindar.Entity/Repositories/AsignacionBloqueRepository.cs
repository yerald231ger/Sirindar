using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class AsignacionBloqueRepository : Repository<AsignacionBloque>, IAsignacionBloqueRepository
    {
        private SirindarDbContext SirindarDbContext
        {
            get { return Context as SirindarDbContext; }
        }

        public AsignacionBloqueRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<IGrouping<string,AsignacionBloque>> GetAsignacionBloquesByMatriucla()
        {
            return SirindarDbContext.AsigancionesBloques
                        .Include(ab => ab.Deporte)
                        .Include(ab => ab.Deportista)
                        .Include(ab => ab.Bloque)
                        .Where(ab => ab.EsActivo)
                        .GroupBy(ab => ab.Deportista.Matricula).ToList();
        }

        public bool IsAsigancionGrupos(int deportistaId, int deporteId, int bloqueId)
        {
            return SingleOrDefault(dd =>
                dd.DeporteId == deporteId
                &&
                dd.DeportistaId == deportistaId
                &&
                dd.BloqueId == bloqueId) != null;
        }
    }
}