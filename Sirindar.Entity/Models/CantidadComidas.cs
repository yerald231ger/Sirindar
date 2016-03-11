using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CNSirindar.Models
{
    [Table("TblCantidadComidas")]
    public class CantidadComidas : TableDbConventions
    {
        [Key, ForeignKey("Deportista")]
        public virtual int DeportistaId { get; set; }

        public virtual NumeroComidas Cantidad { get; set; }

        public virtual bool Lunes { get; set; }

        public virtual bool Martes { get; set; }

        public virtual bool Miercoles { get; set; }

        public virtual bool Jueves { get; set; }

        public virtual bool Viernes { get; set; }

        public virtual bool Sabado { get; set; }

        public virtual bool Domingo { get; set; }

        public virtual Deportista Deportista { get; set; }
    }

    public enum NumeroComidas
    {
        Uno = 1,
        Dos = 2,
        Tres = 3
    }
}