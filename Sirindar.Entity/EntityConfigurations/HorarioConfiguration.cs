using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirindar.Common;

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
