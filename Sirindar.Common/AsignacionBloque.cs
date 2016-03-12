using System;

namespace Sirindar.Common
{
    public class AsignacionBloque : TableDbConventions
    {
        public int AsignacionBloqueId { get; set; }
        public int DeportistaId { get; set; }
        public int DeporteId { get; set; }
        public Nullable<int> BloqueId { get; set; }
        public virtual Deportista Deportista { get; set; }
        public virtual Deporte Deporte { get; set; }
        public virtual Bloque Bloque { get; set; }
    }
}