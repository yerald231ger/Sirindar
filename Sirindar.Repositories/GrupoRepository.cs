using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Repositories
{
    public class GrupoRepository : Repository<Grupo>,  IGrupoRepository
    {
        public GrupoRepository(DbContext context) : base(context)
        {
        }
    }
}