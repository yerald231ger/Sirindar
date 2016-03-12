using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirindar.Core;

namespace Sirindar.Entity.EntityConfigurations
{
    public class EntrenadorConfiguration : EntityTypeConfiguration<Entrenador>
    {
        public EntrenadorConfiguration()
        {
            ToTable("TblEntrenadores");

            HasKey(e => e.EntrenadorId);

            HasMany(d => d.Deportes)
                .WithMany(d => d.Entrenadores);

            HasMany(d => d.Dependencias)
                .WithMany(d => d.Entrenadores);
        }
    }
}
