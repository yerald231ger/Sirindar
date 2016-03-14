using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    class DeporteDeportistaRepository : Repository<DeporteDeportista>, IDeporteDeportistaRepository
    {
        public DeporteDeportistaRepository(DbContext context) : base(context)
        {
        }
    }
}
