using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Repositories
{
    public class DeportistaRepository : Repository<Deportista>, IDeportistaRepository
    {
        public DeportistaRepository(DbContext context) : base(context)
        {
        }
    }
}