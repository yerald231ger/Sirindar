using System.Collections.Generic;
namespace Sirindar.Common
{
    public class Dependencia : TableDbConventions
    {
        public virtual int DependenciaId { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Clave { get; set; }
        public virtual IEnumerable<Deportista> Deportistas { get; set; }
        public virtual IEnumerable<Entrenador> Entrenadores { get; set; }
    }
}
