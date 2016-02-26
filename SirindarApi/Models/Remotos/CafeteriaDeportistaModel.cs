using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirindarApi.Models.Remotos
{
    public class CafeteriaDeportistaModel
    {
        public int DeportistaId { get; set; }
        public string Nombre { get; set; }
        public CafeteriaDepenenciaModel Dependencia { get; set; }
    }

    public class CafeteriaDepenenciaModel
    {
        public string Nombre { get; set; }
    }
}