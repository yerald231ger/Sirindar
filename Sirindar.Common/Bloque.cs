using System.Collections.Generic;
namespace Sirindar.Core
{
    public class Bloque : TableDbConventions
    {
        public int BloqueId { get; set; }
        public string Nombre { get; set; }
        public int KilocaloriasTotales { get; set; }

        public ICollection<Grupo> Grupos { get; set; }
        public ICollection<AsignacionBloque> AsignacionesBloques { get; set; }
    }
}
