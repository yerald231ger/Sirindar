using System.Data.Entity.ModelConfiguration;
using Sirindar.Common;

namespace Sirindar.Entity.EntityConfigurations
{
    public class DeporteDeportistaConfiguration : EntityTypeConfiguration<DeporteDeportista>
    {
        public DeporteDeportistaConfiguration()
        {
            ToTable("TblDeportesDeportistas");

            HasKey(dd => dd.DeporteDeportistaId);

            Property(dd => dd.IniciaEntrenamiento)
                .HasColumnType("Time(7)")
                .IsOptional();

            Property(dd => dd.FinalizaEntrenamiento)
                .HasColumnType("Time(7)")
                .IsOptional();
           
            HasRequired(dd => dd.Deporte)
                .WithMany(d => d.DeportesDeportistas)
                .HasForeignKey(dd => dd.DeporteId);

            HasRequired(dd => dd.Deportista)
                .WithMany(d => d.DeportesDeportistas)
                .HasForeignKey(d => d.DeportistaId);
        }
    }
}
