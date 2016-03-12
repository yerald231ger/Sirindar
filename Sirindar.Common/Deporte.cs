using System.Collections.Generic;
namespace Sirindar.Core
{
    public class Deporte : TableDbConventions
    {
        public int DeporteId { get; set; }
        public string Nombre { get; set; }
        public Energia TipoEnergia { get; set; }
        public int ClasificacionDeporteId { get; set; }

        public ICollection<Deportista> Deportistas { get; set; }

        public virtual ClasificacionDeporte Clasificacion { get; set; }
        public virtual ICollection<Entrenador> Entrenadores { get; set; }
        public virtual ICollection<DeporteDeportista> DeportesDeportistas { get; set; }
        public virtual ICollection<AsignacionBloque> AsignacionesBloques { get; set; }       
    }

    public enum Energia
    {
        Aerobico,
        Anaerobico,
        AerobicoAnaerobico
    }
}
