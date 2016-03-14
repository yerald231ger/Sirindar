using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class BloqueRepository : Repository<Bloque>, IBloqueRepository
    {
        public BloqueRepository(DbContext context) : base(context)
        {
        }
    }
}