using System;
using System.Data.Entity;
using System.Linq;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class DeportistaRepository : Repository<Deportista>, IDeportistaRepository
    {
        private SirindarDbContext sirindarDbContext
        {
            get { return Context as SirindarDbContext; }
        }

        public DeportistaRepository(DbContext context) : base(context) { }

        public Deportista GetDeportistaByMatricula(string matricula)
        {
            return sirindarDbContext.Deportistas.FirstOrDefault(d => d.Matricula == matricula);
        }
    }
}