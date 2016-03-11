using Sirindar.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirindar.Entity.EntityConfigurations
{
    public class ClasificacionDeporteConfiguration : EntityTypeConfiguration<ClasificacionDeporte>
    {
        public ClasificacionDeporteConfiguration() 
        {
            ToTable("TblClasificacionDeporte");

            HasKey(c => c.ClasificacionDeporteId);

            Property(c => c.Descripcion)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.Abreviatura)
                .IsRequired()
                .HasMaxLength(5);

            HasMany(c => c.Deportes)
                .WithRequired(d => d.Clasificaion)
                .HasForeignKey(d => d.ClasificacionDeporteId);
        }

        private object HasMany(Func<ClasificacionDeporte, ICollection<TTargetEntity>> func)
        {
            throw new NotImplementedException();
        }
    }
}
