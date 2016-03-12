using Sirindar.Core;
using System.Data.Entity.ModelConfiguration;

namespace Sirindar.Entity.EntityConfigurations
{
    class AsignacionBloqueConfiguration : EntityTypeConfiguration<AsignacionBloque>
    {
        public AsignacionBloqueConfiguration() 
        {
            ToTable("TblAsignacionBloques");

            HasKey(b => b.AsignacionBloqueId);

            HasRequired(a => a.Deportista)
                .WithMany(d => d.AsignacionesBloques)
                .HasForeignKey(a => a.DeportistaId);

            HasRequired(a => a.Deporte)
                .WithMany(d => d.AsignacionesBloques)
                .HasForeignKey(a => a.DeporteId);

            HasOptional(a => a.Bloque)
                .WithMany(b => b.AsignacionesBloques)
                .HasForeignKey(a => a.BloqueId);
        }
    }
}
