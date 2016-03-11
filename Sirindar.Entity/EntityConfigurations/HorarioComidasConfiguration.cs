using Sirindar.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirindar.Entity.EntityConfigurations
{
    class HorarioComidasConfiguration : EntityTypeConfiguration<HorarioComidas>
    {
        public HorarioComidasConfiguration() 
        {
            ToTable("TblHorarioComidas");

            HasKey(h => h.DeportistaId);

            HasRequired(h => h.Deportista)
                .WithRequiredDependent(d => d.HorarioComidas);
        }
    }
}
