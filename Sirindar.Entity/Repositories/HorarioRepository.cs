using System;
using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;
using System.Linq;

namespace Sirindar.Entity.Repositories
{
    public class HorarioRepository : Repository<Horario>, IHorarioRepository
    {
        private SirindarDbContext sirindarDbContext
        {
            get { return Context as SirindarDbContext; }
        }

        public HorarioRepository(DbContext context) : base(context)
        {
            
        }

        public Horario GetHorarioBetweenHour(DateTime dateTime)
        {
            return GetAll().FirstOrDefault(h => dateTime.TimeOfDay >= h.Inicia && dateTime.TimeOfDay <= h.Finaliza);
        }
    }
}