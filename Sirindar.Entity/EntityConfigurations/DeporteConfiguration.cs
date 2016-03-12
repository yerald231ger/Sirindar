using System.Data.Entity.ModelConfiguration;
using Sirindar.Common;

namespace Sirindar.Entity.EntityConfigurations
{
    public class DeporteConfiguration : EntityTypeConfiguration<Deporte>
    {
        public DeporteConfiguration()
        {
            ToTable("TblDeportes");

            HasKey(d => d.DeporteId);

            Property(d => d.Nombre)
                .HasMaxLength(50);

            Property(d => d.TipoEnergia)
                .IsRequired();

            HasRequired(d => d.Clasificacion)
                .WithMany(c => c.Deportes)
                .HasForeignKey(d => d.ClasificacionDeporteId);
            
            
        }
    }
}
