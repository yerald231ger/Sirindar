using System;
using System.Collections.Generic;
namespace Sirindar.Core
{
    public class Deportista : IPersona
    {
        public int DeportistaId { get; set; }
        public int DependenciaId { get; set; }
        public string Matricula { get; set; }
        public Status Status { get; set; }
        public DateTime FechaRegistro { get; set; }
        public virtual Dependencia Dependencia { get; set; }
        public virtual HorarioComidas HorarioComidas { get; set; }

        public IEnumerable<Deporte> Deportes { get; set; }
        public virtual ICollection<Asistencia> Asistencias { get; set; }
        public virtual ICollection<AsignacionBloque> AsignacionesBloques { get; set; }
        public virtual ICollection<DeporteDeportista> DeportesDeportistas { get; set; }
    }

    public enum Status
    {
        Baja,
        Alta
    }
}
