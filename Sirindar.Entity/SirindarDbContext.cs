using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirindar.Entity
{
    public class SirindarDbContext : IdentityDbContext<ApplicationUser>
    {
        public SirindarDbContext()
            : base("CNSirindarConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Where(t => t.Name == "FechaAlta").Configure(c => c.HasColumnType("datetime"));
            modelBuilder.Properties<DateTime>().Where(t => t.Name == "FechaModificacion").Configure(c => c.HasColumnType("datetime"));
            modelBuilder.Properties<DateTime>().Where(t => t.Name == "FechaNacimiento").Configure(c => c.HasColumnType("date"));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<ClasificacionDeporte> ClasificacionesDeportes { get; set; }
        public DbSet<Dependencia> Dependencias { get; set; }
        public DbSet<Deporte> Deportes { get; set; }
        public DbSet<Deportista> Deportistas { get; set; }
        public DbSet<Entrenador> Entrenadores { get; set; }
        public DbSet<Bloque> Bloques { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<GrupoAlimenticio> GruposAlimenticios { get; set; }
        public DbSet<DeporteDeportista> DeportesDeportistas { get; set; }
        public DbSet<CantidadComidas> CantidadComidas { get; set; }
        public DbSet<AsignacionBloque> AsigancionesBloques { get; set; }
    }

    public class SirindarDbContextInizializer : CreateDatabaseIfNotExists<SirindarDbContext>
    {
        protected override void Seed(SirindarDbContext context)
        {
            context.Roles.Add(new IdentityRole
            {
                Id = "A",
                Name = "Admin"
            });
            context.Roles.Add(new IdentityRole
            {
                Id = "U",
                Name = "User"
            });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
