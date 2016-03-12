using System;
using Sirindar.Core.Repositories;

namespace Sirindar.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository Usuarios { get; set; }
        IAsignacionBloqueRepository AsignacionesBloques { get; set; }
        IAsistenciaRepository Asistencias { get; set; }
        IBloqueRepository Bloques { get; set; }
        IClasificacionDeporteRepository ClasificacionesDeportes { get; set; }
        IDependenciaRepository Dependencias { get; set; }
        IDeporteRepository Deportes { get; set; }
        IDeporteDeportistaRepository DeportesDeportistas { get; set; }
        IDeportistaRepository Deportistas { get; set; }
        IEntrenadorRepository Entrenadores { get; set; }
        IGrupoAlimenticioRepository GruposAlimenticios { get; set; }
        IGrupoRepository Grupos { get; set; }
        IHorarioComidasRepository HorariosComidas { get; set; }
        IHorarioRepository Horarios { get; set; }
        int Complete();
    }
}