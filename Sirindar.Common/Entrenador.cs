using System.Collections.Generic;
namespace Sirindar.Common
{
    public class Entrenador : Persona
    {
        public int EntrenadorId { get; set; }

        public IEnumerable<Deporte> Deportes { get; set; }
        public IEnumerable<Dependencia> Dependencias { get; set; }
    }
}
