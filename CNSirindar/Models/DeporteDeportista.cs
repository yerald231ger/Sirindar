using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CNSirindar.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data.Entity.Infrastructure;
using System;

namespace CNSirindar.Models
{
    [Table("TblDeportesDeportistas")]
    public class DeporteDeportista : TableDbConventions
    {
        public int DeporteDeportistaId { get; set; }

        public virtual int DeportistaId { get; set; }
        public virtual int DeporteId { get; set; }

        public Deportista Deportista { get; set; }
        public Deporte Deporte { get; set; }
        
        public Nullable<TimeSpan> IniciaEntrenamiento { get; set; }
        public Nullable<TimeSpan> FinalizaEntrenamiento { get; set; }

    }
}