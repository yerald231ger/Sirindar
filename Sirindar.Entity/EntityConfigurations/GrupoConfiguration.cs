using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirindar.Core;

namespace Sirindar.Entity.EntityConfigurations
{
    public class GrupoConfiguration : EntityTypeConfiguration<Grupo>
    {
        public GrupoConfiguration()
        {
            ToTable("TblGrupos");

            HasKey(g => g.GrupoId);

            Property(g => g.Nombre)
                .IsRequired();

            Property(d => d.Porcentaje)
                .IsRequired();

            Property(d => d.Gramos)
                .IsRequired();

            Property(d => d.Kilocalorias)
                .IsRequired();

            Property(d => d.Equivalencias)
                .HasMaxLength(50)
                .IsRequired();

            HasRequired(g => g.GrupoAlimenticio)
                .WithMany(ga => ga.Grupos)
                .HasForeignKey(g => g.GrupoAlimenticioId);

            HasRequired(g => g.Bloque)
                .WithMany(b => b.Grupos)
                .HasForeignKey(g => g.BloqueId);
        }
    }
}
