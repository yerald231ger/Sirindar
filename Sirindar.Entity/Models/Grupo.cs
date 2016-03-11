using System;
using System.Linq;
//using System.Data;
using System.Data.Linq;
using CNSirindar.Extensions;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CNSirindar.Models;

namespace CNSirindar.Models
{
    [Table("TblGrupos")]
    public class Grupo : TableDbConventions
    {
        public virtual int GrupoId { get; set; }

        [Required]
        public virtual string Nombre { get; set; }

        [Required]
        public virtual int Porcentaje { get; set; }

        [Required]
        public virtual int Gramos { get; set; }

        [Required]
        public virtual int Kilocalorias { get; set; }

        [Required]
        [MaxLength(500)]
        public virtual string Equivalencias { get; set; }

        public virtual int BloqueId { get; set; }

        public virtual int GrupoAlimenticioId { get; set; }

        public virtual GrupoAlimenticio GrupoAlimenticio { get; set; }

        public virtual Bloque Bloque { get; set; }


        
    }
}
