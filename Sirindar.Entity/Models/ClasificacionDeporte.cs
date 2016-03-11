using CNSirindar.Extensions;
using System;
using System.Linq;
using System.Data.Linq;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CNSirindar.Models
{
    [Table("TblClasificacionesDeportes")]
    public class ClasificacionDeporte : TableDbConventions
    {
         public ClasificacionDeporte()
        {
            this.Deportes = new HashSet<Deporte>();
        }

        public virtual int ClasificacionDeporteId { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Descripcion { get; set; }

        [Required]
        [MaxLength(5)]
        public virtual string Abreviatura { get; set; }
        public virtual ICollection<Deporte> Deportes { get; set; }        
    }
}
