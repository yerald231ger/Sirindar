using System.Collections.Generic;
namespace Sirindar.Common
{
    public class GrupoAlimenticio : TableDbConventions
    {
        public int GrupoAlimenticioId { get; set; }
        public string Grupo { get; set; }
        public virtual ICollection<Grupo> Grupos { get; set; } 

    }
    
}