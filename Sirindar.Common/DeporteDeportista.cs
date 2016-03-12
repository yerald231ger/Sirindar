using System;
namespace Sirindar.Core
{
    public class DeporteDeportista : TableDbConventions
    {
        public int DeporteDeportistaId { get; set; }

        public int DeportistaId { get; set; }
        public int DeporteId { get; set; }

        public virtual Deportista Deportista { get; set; }
        public virtual Deporte Deporte { get; set; }
        
        public TimeSpan IniciaEntrenamiento { get; set; }
        public TimeSpan FinalizaEntrenamiento { get; set; }

    }
}