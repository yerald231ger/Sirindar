using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Repositories
{
    public class HorarioComidasRepository : Repository<HorarioComidas>, IHorarioComidasRepository
    {
        public HorarioComidasRepository(DbContext context) : base(context)
        {
        }
    }
}