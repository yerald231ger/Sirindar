using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SirindarApi.Models.Remotos
{
    public class RegistrarAsistenciaViewModel
    {
        [Required]
        public DateTime HoraAsistencia { get; set; }

        [Required]
        public int DeportistaId { get; set; }

        [Required]
        public int HorarioId { get; set; }
    }
}