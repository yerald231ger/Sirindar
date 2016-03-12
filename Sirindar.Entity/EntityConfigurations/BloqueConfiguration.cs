using Sirindar.Common;
using System.Data.Entity.ModelConfiguration;

namespace Sirindar.Entity.EntityConfigurations
{
    public class BloqueConfiguration : EntityTypeConfiguration<Bloque>
    {
        public BloqueConfiguration() 
        {
            ToTable("TblBloques");

            HasKey(b => b.BloqueId);

            Property(b => b.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            Property(b => b.KilocaloriasTotales)
                .IsOptional();

            HasMany(b => b.Grupos)
                .WithRequired(g => g.Bloque)
                .HasForeignKey(g => g.BloqueId);
            
        }
    }
}
