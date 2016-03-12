using System.Data.Entity.ModelConfiguration;

namespace Sirindar.Entity.EntityConfigurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(u => u.Email)
                .IsRequired();
        }
    }
}
