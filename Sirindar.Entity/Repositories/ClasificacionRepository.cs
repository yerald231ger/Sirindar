﻿using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Entity.Repositories
{
    public class ClasificacionRepository : Repository<ClasificacionDeporte>, IClasificacionDeporteRepository
    {
        public ClasificacionRepository(DbContext context) : base(context)
        {
        }
    }
}