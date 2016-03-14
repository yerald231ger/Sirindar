using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class DependenciaRepository : Repository<Dependencia>, IDependenciaRepository
    {
        public DependenciaRepository(DbContext context) : base(context)
        {
        }
    }
}