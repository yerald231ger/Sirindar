using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using Sirindar.Core;
using Sirindar.Entity.EntityConfigurations;

namespace Sirindar.Entity
{
    public class SirindarDbContext : IdentityDbContext<ApplicationUser>
    {
        public SirindarDbContext()
            : base("SirindarConnectionString")
        {
            Configuration.LazyLoadingEnabled = true;
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

        public List<TEntity> EjecutaSp<TEntity>(string storeProcedure, IDictionary<string, string> parametros = null) where TEntity : class
        {
            if (parametros == null)
                return Database.SqlQuery<TEntity>(storeProcedure).ToList();

            var parameters = new object[parametros.Count];
            var count = 0;
            foreach (var kvp in parametros)
            {
                parameters[count] = new SqlParameter(kvp.Key, kvp.Value);
                storeProcedure += string.Format("{0} {1}", count > 0 ? "," : "", kvp.Key);
                count++;
            }
            return Database.SqlQuery<TEntity>(storeProcedure, parameters).ToList();
        }
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
