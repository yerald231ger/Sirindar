using System.Collections.Generic;
namespace Sirindar.Core
{
    public class Entrenador : Persona
    {
        public int EntrenadorId { get; set; }

        public virtual ICollection<Deporte> Deportes { get; set; }
        public virtual ICollection<Dependencia> Dependencias { get; set; }
    }
}
