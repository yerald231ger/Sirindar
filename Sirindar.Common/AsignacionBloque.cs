namespace Sirindar.Common
{
    public class AsignacionBloque : TableDbConventions
    {
        public int AsignacionBloqueId { get; set; }
        public virtual int DeportistaId { get; set; }
        public virtual int DeporteId { get; set; }
        public virtual int BloqueId { get; set; }
        public virtual Deportista Deportista { get; set; }
        public virtual Deporte Deporte { get; set; }
        public virtual Bloque Bloque { get; set; }
    }
}