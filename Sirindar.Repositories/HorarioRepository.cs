using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Repositories
{
    public class HorarioRepository : Repository<Horario>, IHorarioRepository
    {
        public HorarioRepository(DbContext context) : base(context)
        {
        }
    }
}