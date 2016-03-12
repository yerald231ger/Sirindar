using Sirindar.Core;
using System.Data.Entity.ModelConfiguration;

namespace Sirindar.Entity.EntityConfigurations
{
    public class ClasificacionDeporteConfiguration : EntityTypeConfiguration<ClasificacionDeporte>
    {
        public ClasificacionDeporteConfiguration() 
        {
            ToTable("TblClasificacionDeportes");

            HasKey(c => c.ClasificacionDeporteId);

            Property(c => c.Descripcion)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.Abreviatura)
                .IsRequired()
                .HasMaxLength(5);

            HasMany(c => c.Deportes)
                .WithRequired(d => d.Clasificacion)
                .HasForeignKey(d => d.ClasificacionDeporteId);
        }

    }
}
