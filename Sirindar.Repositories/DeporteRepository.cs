using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Repositories
{
    public class DeporteRepository : Repository<Deporte>, IDeporteRepository
    {
        public DeporteRepository(DbContext context) : base(context)
        {
        }
    }
}