﻿using System.Data.Entity;
using Sirindar.Core;
using Sirindar.Core.Repositories;

namespace Sirindar.Repositories
{
    public class AsignacionBloqueRepository : Repository<AsignacionBloque>, IAsignacionBloqueRepository
    {
        public AsignacionBloqueRepository(DbContext context) : base(context)
        {
        }
    }
}