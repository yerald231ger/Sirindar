using System.Collections.Generic;

namespace Sirindar.Core.Repositories
{
    public interface IBloqueRepository : IRepository<Bloque,int>
    {
        Bloque GetWithGrupos(int blqoueId);
        bool IsBloque(int bloqueId);
        void SumaKilocalorias(int bloqueId);
    }
}
