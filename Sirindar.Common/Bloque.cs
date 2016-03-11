using System.Collections.Generic;
namespace Sirindar.Common
{
    public class Bloque : TableDbConventions
    {
        public int BloqueId { get; set; }
        public string Nombre { get; set; }
        public int KilocaloriasTotales { get; set; }

        public IEnumerable<Grupo> Grupos { get; set; }
        public IEnumerable<AsignacionBloque> AsignacionesBloques { get; set; }
    }
}
