using System.Data.Entity;
using Sirindar.Core;

namespace Sirindar.Repositories
{
    public class SirindarDbContext : DbContext
    {
        public SirindarDbContext() : base("SirindarConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<Asistencia> Asistencias { get; set; }
        public virtual DbSet<ClasificacionDeporte> ClasificacionesDeportes { get; set; }
        public virtual DbSet<Dependencia> Dependencias { get; set; }
        public virtual DbSet<Deporte> Deportes { get; set; }
        public virtual DbSet<Deportista> Deportistas { get; set; }
        public virtual DbSet<Entrenador> Entrenadores { get; set; }
        public virtual DbSet<Bloque> Bloques { get; set; }
        public virtual DbSet<Grupo> Grupos { get; set; }
        public virtual DbSet<Horario> Horarios { get; set; }
        public virtual DbSet<GrupoAlimenticio> GruposAlimenticios { get; set; }
        public virtual DbSet<DeporteDeportista> DeportesDeportistas { get; set; }
        public virtual DbSet<HorarioComidas> HorariosComidas { get; set; }
        public virtual DbSet<AsignacionBloque> AsigancionesBloques { get; set; }
    }
}
