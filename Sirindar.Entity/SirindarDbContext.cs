using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sirindar.Common;
using Sirindar.Entity.EntityConfigurations;

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
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new AsignacionBloqueConfiguration());
            modelBuilder.Configurations.Add(new AsistenciaConfiguration());
            modelBuilder.Configurations.Add(new BloqueConfiguration());
            modelBuilder.Configurations.Add(new ClasificacionDeporteConfiguration());
            modelBuilder.Configurations.Add(new DependenciaConfiguration());
            modelBuilder.Configurations.Add(new DeporteConfiguration());
            modelBuilder.Configurations.Add(new DeporteDeportistaConfiguration());
            modelBuilder.Configurations.Add(new DeportistaConfiguration());
            modelBuilder.Configurations.Add(new EntrenadorConfiguration());
            modelBuilder.Configurations.Add(new GrupoAlimenticioConfiguration());
            modelBuilder.Configurations.Add(new GrupoConfiguration());
            modelBuilder.Configurations.Add(new HorarioComidasConfiguration());
            modelBuilder.Configurations.Add(new HorarioConfiguration());

            modelBuilder.Properties<DateTime>().Where(t => t.Name == "FechaAlta").Configure(c => c.HasColumnType("datetime"));
            modelBuilder.Properties<DateTime>().Where(t => t.Name == "FechaModificacion").Configure(c => c.HasColumnType("datetime"));
            modelBuilder.Properties<DateTime>().Where(t => t.Name == "FechaNacimiento").Configure(c => c.HasColumnType("date"));
            modelBuilder.Properties<string>().Where(t => t.Name == "Nombre").Configure(c => c.IsRequired());
            modelBuilder.Properties<string>().Where(t => t.Name == "Appellidos").Configure(c => c.IsRequired());
            modelBuilder.Properties<Generos>().Configure(c => c.IsRequired());
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
        public DbSet<HorarioComidas> CantidadComidas { get; set; }
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
