using System.Data.Entity.ModelConfiguration;
using Sirindar.Core;

namespace Sirindar.Entity.EntityConfigurations
{
    public class HorarioConfiguration : EntityTypeConfiguration<Horario>
    {
        public HorarioConfiguration()
        {
            ToTable("TblHorarios");

            HasKey(h => h.HorarioId);

            Property(h => h.Nombre)
                .IsRequired();

            Property(h => h.Inicia)
                .HasColumnType("Time")
                .IsRequired();

            Property(h => h.Finaliza)
                .HasColumnType("Time")
                .IsRequired();
        }
    }
}
