﻿using System;
using System.Collections.Generic;
namespace Sirindar.Common
{
    public class Deportista : Persona
    {
        public int DeportistaId { get; set; }
        public int DependenciaId { get; set; }
        public string Matricula { get; set; }
        public Status Status { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Dependencia Dependencia { get; set; }
        public CantidadComidas CantidadComidas { get; set; }

        public virtual IEnumerable<Deporte> Deportes { get; set; }
        public IEnumerable<Asistencia> Asistencias { get; set; }
        public IEnumerable<AsignacionBloque> AsignacionesBloques { get; set; }
        public IEnumerable<DeporteDeportista> DeportesDeportistas { get; set; }
    }

    public enum Status
    {
        Baja,
        Alta
    }
}
