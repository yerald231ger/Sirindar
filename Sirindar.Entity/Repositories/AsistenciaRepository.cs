using System.Collections.Generic;
using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class AsistenciaRepository : Repository<Asistencia>, IAsistenciaRepository
    {
        public AsistenciaRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Asistencia> GetAsistenciaByDeportistaId(int deportistaId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Asistencia> GetAsistenciaByDeportistaMatricualId(string matricula)
        {
            throw new System.NotImplementedException();
        }
    }
}