using System.Data.Entity;

namespace Sirindar.Repositories
{
    public class SirindarDbContext : DbContext
    {
        public SirindarDbContext() : base("SirindarConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    }
}
