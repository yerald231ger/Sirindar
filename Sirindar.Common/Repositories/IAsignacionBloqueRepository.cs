using System.Collections.Generic;
using System.Linq;

namespace Sirindar.Core.Repositories
{
    public interface IAsignacionBloqueRepository : IRepository<AsignacionBloque,int>
    {
        IEnumerable<IGrouping<string, AsignacionBloque>> GetAsignacionBloquesByMatriucla();
        bool IsAsigancionGrupos(int deporteId, int deportistaId, int bloqueId);
    }
}
