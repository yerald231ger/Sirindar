using System;
using System.Linq;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CNSirindar.Models;
using CNSirindar.Extensions;

namespace CNSirindar.Models
{
    [Table("TblBloques")]
    public class Bloque : TableDbConventions
    {
        public Bloque()
        {
            this.Grupos = new HashSet<Grupo>();
            this.AsignacionesBloques = new HashSet<AsignacionBloque>();
        }

        public virtual int BloqueId { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        public virtual string Nombre { get; set; }

        public virtual Nullable<int> KilocaloriasTotales { get; set; }

        public virtual ICollection<Grupo> Grupos { get; set; }
        public virtual ICollection<AsignacionBloque> AsignacionesBloques { get; set; }
    }
}
