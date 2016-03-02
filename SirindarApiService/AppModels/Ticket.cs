using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNSirindar.Models;

namespace ServiciosCafeteria.AppModels
{
    public class Ticket 
    {
        public DateTime Fecha { get; set; }
        public string Deportista { get; set; }
        public string Comida { get; set; }
        public string Deporte { get; set; }
        public string Dependencia { get; set; }
        public string GrupoRaciones { get; set; }
        public int KiloCalorias { get; set; }
        public int GramosProteina { get; set; }
        public int GramosCarboHidratos { get; set; }
        public int GramosLipidos { get; set; }
        public string Observaciones { get; set; }
    }

    public class Equipo
    {
        public string Nombre { get; set; }
        public int Id { get; set; }
    }
}
