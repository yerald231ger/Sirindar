using Sirindar.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirindar.Entity.EntityConfigurations
{
    class AsignacionBloqueConfiguration : EntityTypeConfiguration<AsignacionBloque>
    {
        public AsignacionBloqueConfiguration() 
        {
            ToTable("TblAsignacionBloques");

            HasKey(b => b.AsignacionBloqueId);

            HasRequired<Deportista>(a => a.Deportista)
                .WithMany(d => d.AsignacionesBloques)
                .HasForeignKey(a => a.DeportistaId);

            HasRequired<Deporte>(a => a.Deporte)
                .WithMany(d => d.AsignacionesBloques)
                .HasForeignKey(a => a.DeporteId);

            HasOptional<Bloque>(a => a.Bloque)
                .WithMany(b => b.AsignacionesBloques)
                .HasForeignKey(a => a.BloqueId);
        }
    }
}
