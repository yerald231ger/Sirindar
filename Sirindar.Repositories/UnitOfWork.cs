using System;
using Sirindar.Core.Repositories;
using Sirindar.Core.UnitOfWork;

namespace Sirindar.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SirindarDbContext _context;

        public UnitOfWork(SirindarDbContext context)
        {
            _context = context;
            Usuarios = new UsuarioRepository();
            AsignacionesBloques = new AsignacionBloqueRepository(_context);
            Asistencias = new AsistenciaRepository(_context);
            Bloques = new BloqueRepository(_context);
            ClasificacionesDeportes = new ClasificacionRepository(_context);
            Dependencias = new DependenciaRepository(_context);
            Deportes = new DeporteRepository(_context);
            Deportistas = new DeportistaRepository(_context);
            DeportesDeportistas = new DeporteDeportistaRepository(_context);
            Entrenadores = new EntrenadorRepository(_context);
            GruposAlimenticios = new GrupoAlimenticioRepository(_context);
            Grupos = new GrupoRepository(_context);
            HorariosComidas = new HorarioComidasRepository(_context);
            Horarios = new HorarioRepository(_context);
        }

        public IUsuarioRepository Usuarios { get; set; }
        public IAsignacionBloqueRepository AsignacionesBloques { get; set; }
        public IAsistenciaRepository Asistencias { get; set; }
        public IBloqueRepository Bloques { get; set; }
        public IClasificacionDeporteRepository ClasificacionesDeportes { get; set; }
        public IDependenciaRepository Dependencias { get; set; }
        public IDeporteRepository Deportes { get; set; }
        public IDeporteDeportistaRepository DeportesDeportistas { get; set; }
        public IDeportistaRepository Deportistas { get; set; }
        public IEntrenadorRepository Entrenadores { get; set; }
        public IGrupoAlimenticioRepository GruposAlimenticios { get; set; }
        public IGrupoRepository Grupos { get; set; }
        public IHorarioComidasRepository HorariosComidas { get; set; }
        public IHorarioRepository Horarios { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
