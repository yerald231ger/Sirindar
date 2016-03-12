using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Repositories
{
    class DeporteDeportistaRepository : Repository<DeporteDeportista>, IDeporteDeportistaRepository
    {
        public DeporteDeportistaRepository(DbContext context) : base(context)
        {
        }
    }
}
