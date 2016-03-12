using System.Collections.Generic;
namespace Sirindar.Common
{
    public class Entrenador : Persona
    {
        public int EntrenadorId { get; set; }

        public ICollection<Deporte> Deportes { get; set; }
        public ICollection<Dependencia> Dependencias { get; set; }
    }
}
