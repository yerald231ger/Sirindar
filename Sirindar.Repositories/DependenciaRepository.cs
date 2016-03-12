using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Repositories
{
    public class DependenciaRepository : Repository<Dependencia>, IDependenciaRepository
    {
        public DependenciaRepository(DbContext context) : base(context)
        {
        }
    }
}