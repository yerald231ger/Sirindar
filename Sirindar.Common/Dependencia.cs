using System.Collections.Generic;
namespace Sirindar.Common
{
    public class Dependencia : TableDbConventions
    {
        public int DependenciaId { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public virtual ICollection<Deportista> Deportistas { get; set; }
        public virtual ICollection<Entrenador> Entrenadores { get; set; }
    }
}
