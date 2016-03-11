using System;
namespace Sirindar.Common
{
    public class DeporteDeportista : TableDbConventions
    {
        public int DeporteDeportistaId { get; set; }

        public int DeportistaId { get; set; }
        public int DeporteId { get; set; }

        public Deportista Deportista { get; set; }
        public Deporte Deporte { get; set; }
        
        public TimeSpan IniciaEntrenamiento { get; set; }
        public TimeSpan FinalizaEntrenamiento { get; set; }

    }
}