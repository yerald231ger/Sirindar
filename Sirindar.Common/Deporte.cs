using System.Collections.Generic;
namespace Sirindar.Common
{
    public class Deporte : TableDbConventions
    {
        public int DeporteId { get; set; }
        public string Nombre { get; set; }
        public Energia TipoEnergia { get; set; }
        public int ClasificacionDeporteId { get; set; }

        public ClasificacionDeporte Clasificacion { get; set; }
        public virtual IEnumerable<Deportista> Deportistas { get; set; }
        public IEnumerable<Entrenador> Entrenadores { get; set; }
        public IEnumerable<DeporteDeportista> DeportesDeportistas { get; set; }
        public IEnumerable<AsignacionBloque> AsignacionesBloques { get; set; }       
    }

    public enum Energia
    {
        Aerobico,
        Anaerobico,
        AerobicoAnaerobico
    }
}
