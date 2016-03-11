using Sirindar.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirindar.Entity.EntityConfigurations
{
    public class BloqueConfiguration : EntityTypeConfiguration<Bloque>
    {
        public BloqueConfiguration() 
        {
            ToTable("TblBloques");

            HasKey(b => b.BloqueId);

            Property(b => b.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            Property(b => b.KilocaloriasTotales)
                .IsOptional();

            HasMany(b => b.Grupos)
                .WithRequired(g => g.Bloque)
                .HasForeignKey(g => g.BloqueId);

            HasMany(b => b.AsignacionesBloques)
                .WithOptional(a => a.Bloque)
                .HasForeignKey(a => a.BloqueId);
        }
    }
}
