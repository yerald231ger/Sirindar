using System.Collections.Generic;

namespace Sirindar.Core.Repositories
{
    public interface IAsistenciaRepository : IRepository<Asistencia, int>
    {
        IEnumerable<Asistencia> GetAsistenciaByDeportistaId(int deportistaId);
        IEnumerable<Asistencia> GetAsistenciaByDeportistaMatricualId(string matricula);
        IEnumerable<Asistencia> GetAsistenciasDeportistaToday(int deportistaId);
    }
}
