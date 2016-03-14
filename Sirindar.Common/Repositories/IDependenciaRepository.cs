using System.Collections.Generic;

namespace Sirindar.Core.Repositories
{
    public interface IDependenciaRepository : IRepository<Dependencia, int>
    {
        IEnumerable<Dependencia> SearchByNombre(string dependencia);
    }
}