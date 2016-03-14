using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class EntrenadorRepository : Repository<Entrenador>,  IEntrenadorRepository
    {
        public EntrenadorRepository(DbContext context) : base(context)
        {
        }
    }
}