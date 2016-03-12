using System.Collections.Generic;
namespace Sirindar.Core
{
    public class GrupoAlimenticio : TableDbConventions
    {
        public int GrupoAlimenticioId { get; set; }
        public string Grupo { get; set; }
        public virtual ICollection<Grupo> Grupos { get; set; } 

    }
    
}