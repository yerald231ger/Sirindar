using Sirindar.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirindar.Entity.EntityConfigurations
{
    public class DependenciaConfiguration : EntityTypeConfiguration<Dependencia>
    {
        public DependenciaConfiguration() 
        {
            ToTable("TblDependencias");

            HasKey(d => d.DependenciaId);

            Property(d => d.Nombre)
                .IsRequired();

            Property(d => d.Clave)
                .IsRequired();

            HasMany(d => d.Deportistas)
                .WithRequired(dprt => dprt.Dependencia)
                .HasForeignKey(dprt => dprt.DependenciaId);
            
        }
    }
}
