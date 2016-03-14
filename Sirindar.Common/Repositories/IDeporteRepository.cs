using System.Collections.Generic;

namespace Sirindar.Core.Repositories
{
    public interface IDeporteRepository : IRepository<Deporte, int>
    {
        IEnumerable<Deporte> SearchByNombre(string deporte);
    }
}