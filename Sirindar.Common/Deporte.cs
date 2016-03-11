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
        public virtual ICollection<Deportista> Deportistas { get; set; }
        public ICollection<Entrenador> Entrenadores { get; set; }
        public ICollection<DeporteDeportista> DeportesDeportistas { get; set; }
        public ICollection<AsignacionBloque> AsignacionesBloques { get; set; }       
    }

    public enum Energia
    {
        Aerobico,
        Anaerobico,
        AerobicoAnaerobico
    }
}
