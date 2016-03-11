namespace Sirindar.Common
{
    public class Grupo : TableDbConventions
    {
        public int GrupoId { get; set; }
        public string Nombre { get; set; }
        public int Porcentaje { get; set; }
        public int Gramos { get; set; }
        public int Kilocalorias { get; set; }
        public string Equivalencias { get; set; }
        public int BloqueId { get; set; }
        public int GrupoAlimenticioId { get; set; }
        public GrupoAlimenticio GrupoAlimenticio { get; set; }
        public Bloque Bloque { get; set; }        
    }
}
