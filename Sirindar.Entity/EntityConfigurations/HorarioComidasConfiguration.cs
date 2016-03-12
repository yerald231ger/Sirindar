using Sirindar.Core;
using System.Data.Entity.ModelConfiguration;

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
