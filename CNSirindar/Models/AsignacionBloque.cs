using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CNSirindar.Models
{
    [Table("TblAsignacionesBloques")]
    public class AsignacionBloque : TableDbConventions
    {
        public int AsignacionBloqueId { get; set; }

        public virtual int DeportistaId { get; set; }
        public virtual int DeporteId { get; set; }
        public virtual Nullable<int> BloqueId { get; set; }

        public virtual Deportista Deportista { get; set; }
        public virtual Deporte Deporte { get; set; }
        public virtual Bloque Bloque { get; set; }
    }
}