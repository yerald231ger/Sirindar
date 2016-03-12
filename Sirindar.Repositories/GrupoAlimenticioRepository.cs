using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Repositories
{
    public class GrupoAlimenticioRepository : Repository<GrupoAlimenticio>, IGrupoAlimenticioRepository
    {
        public GrupoAlimenticioRepository(DbContext context) : base(context)
        {
        }
    }
}