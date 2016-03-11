using System;
using System.ComponentModel.DataAnnotations;

namespace CNSirindar.Models
{
    public class TableDbConventions
    {
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> FechaAlta { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<DateTime> FechaModificacion { get; set; } 

        public bool EsActivo { get; set; }
    }
}
