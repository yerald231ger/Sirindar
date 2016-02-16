using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CNSirindar.Models;
using CNSirindar.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CNSirindar.Models
{
    [Table("TblHorarios")]
    public class Horario : TableDbConventions
    {
        public int HorarioId { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public ComidasDia Nombre { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public virtual TimeSpan Inicia { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public virtual TimeSpan Finaliza { get; set; }      
    }

    public enum ComidasDia
    {
        Desayuno,
        Comida,
        Cena
    }
}