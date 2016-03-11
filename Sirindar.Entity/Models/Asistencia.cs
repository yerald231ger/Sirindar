using System;
using System.Linq;
using System.Data.Entity;
using System.Collections;
using CNSirindar.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using CNSirindar;
using Newtonsoft.Json;
using Sirindar.Common;

namespace CNSirindar.Models
{
    [Table("TblAsistencias")]
    public class Asistencia_ : Sirindar.Common.Asistencia
    {
        public int AsistenciaId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime HoraAsistencia { get; set; }

        

        public int DeportistaId { get; set; }

        public int HorarioId { get; set; }
        
        [JsonIgnore]
        [IgnoreDataMember]
        public Deportista Deportista { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Horario Horario { get; set; }        
    
    }
}
