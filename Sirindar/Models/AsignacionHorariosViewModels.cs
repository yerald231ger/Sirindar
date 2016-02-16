using CNSirindar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sirindar.Models
{
    public class AsignacionHorariosViewModel
    {
        public string Nombre { get; set; }

        [Required]
        public int Id { get; set; }
        
        [Range(1,3)]
        public NumeroComidas Numero { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Saturday { get; set; }

        public bool Sunday { get; set; }
    }
}