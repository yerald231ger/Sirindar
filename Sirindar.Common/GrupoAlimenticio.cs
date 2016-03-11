using System.Collections.Generic;
namespace Sirindar.Common
{
    public class GrupoAlimenticio : TableDbConventions
    {
        public virtual int GrupoAlimenticioId { get; set; }
        public virtual string Grupo { get; set; }
        public virtual IEnumerable<Grupo> Grupos { get; set; } 

    }
    
}