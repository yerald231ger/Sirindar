using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class AsistenciaRepository : Repository<Asistencia>, IAsistenciaRepository
    {
        private SirindarDbContext sirindarDbContext
        {
            get { return Context as SirindarDbContext; }
        }

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

        public IEnumerable<Asistencia> GetAsistenciasDeportistaToday(int deportistaId)
        {
            return sirindarDbContext.Asistencias.Where(a => a.EsActivo && a.HoraAsistencia > DateTime.Today && a.DeportistaId == deportistaId).ToList();
        }
    }
}