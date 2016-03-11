using System;
using CNSirindar.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CNSirindar.Models
{
    [Table("TblGruposAlimenticios")]
    public class GrupoAlimenticio : TableDbConventions
    {
        public GrupoAlimenticio() 
        {
            this.Grupos = new HashSet<Grupo>();
        }

        public virtual int GrupoAlimenticioId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public virtual string Grupo { get; set; }

        public virtual ICollection<Grupo> Grupos { get; set; }        

        public static IList<GrupoAlimenticio> List()
        {
            var list = new List<GrupoAlimenticio>();
            using (var db = new SirindarDbContext())
            {
                db.GruposAlimenticios.WhereIsActive().ForEach(ga =>
                        list.Add(new GrupoAlimenticio {
                            Grupo = ga.Grupo,
                            GrupoAlimenticioId = ga.GrupoAlimenticioId
                        })
                    );
            }
            return list;
        }        

    }
    
}