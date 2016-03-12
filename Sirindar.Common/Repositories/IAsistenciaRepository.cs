using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirindar.Core.Repositories
{
    public interface IAsistenciaRepository : IRepository<Asistencia, int>
    {
        IEnumerable<Asistencia> GetAsistenciaByDeportistaId(int deportistaId);
        IEnumerable<Asistencia> GetAsistenciaByDeportistaMatricualId(string matricula);
    }
}
