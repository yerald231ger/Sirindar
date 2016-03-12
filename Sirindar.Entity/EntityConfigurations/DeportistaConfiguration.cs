using System.Data.Entity.ModelConfiguration;
using Sirindar.Core;

namespace Sirindar.Entity.EntityConfigurations
{
    public class DeportistaConfiguration : EntityTypeConfiguration<Deportista>
    {
        public DeportistaConfiguration()
        {
            ToTable("TblDeportistas");

            HasKey(d => d.DeportistaId);
            

            Property(d => d.Matricula)
                .HasMaxLength(50)
                .IsRequired();

            Property(d => d.Status)
                .IsRequired();

            Property(d => d.FechaRegistro)
                .HasColumnType("DateTime")
                .IsOptional();

            HasRequired(d => d.Dependencia)
                .WithMany(dp => dp.Deportistas)
                .HasForeignKey(d => d.DependenciaId);

            HasRequired(d => d.HorarioComidas)
                .WithRequiredPrincipal(d => d.Deportista);
            
            
        }
    }
}
