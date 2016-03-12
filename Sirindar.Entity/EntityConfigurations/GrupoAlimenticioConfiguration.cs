using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirindar.Common;

namespace Sirindar.Entity.EntityConfigurations
{
    public class GrupoAlimenticioConfiguration : EntityTypeConfiguration<GrupoAlimenticio>
    {
        public GrupoAlimenticioConfiguration()
        {
            ToTable("TblGruposAlimenticios");

            HasKey(ga => ga.GrupoAlimenticioId);

            Property(ga => ga.Grupo)
                .IsRequired();

            HasMany(ga => ga.Grupos)
                .WithRequired(g => g.GrupoAlimenticio)
                .HasForeignKey(g => g.GrupoAlimenticioId);
        }
    }
}
