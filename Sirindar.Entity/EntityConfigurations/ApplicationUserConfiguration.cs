using System.Data.Entity.ModelConfiguration;
using Sirindar.Common;

namespace Sirindar.Entity.EntityConfigurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(u => u.Email)
                .IsRequired();

            Property(u => u.FechaModificacion)
                .HasColumnType("DateTime");

            Property(u => u.FechaAlta)
                .HasColumnType("DateTime");

            Property(u => u.EsActivo)
                .IsRequired();
        }
    }
}
