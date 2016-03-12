using System.Data.Entity.ModelConfiguration;
using Sirindar.Core;

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
