using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class HorarioComidasRepository : Repository<HorarioComidas>, IHorarioComidasRepository
    {
        private SirindarDbContext SirindarDbContext
        {
            get { return Context as SirindarDbContext; }
        }

        public HorarioComidasRepository(DbContext context) : base(context)
        {
        }
    }
}