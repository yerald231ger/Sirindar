﻿using System.Collections.Generic;
namespace Sirindar.Common
{
    public class ClasificacionDeporte : TableDbConventions
    {
        public int ClasificacionDeporteId { get; set; }
        public string Descripcion { get; set; }
        public string Abreviatura { get; set; }
        public ICollection<Deporte> Deportes { get; set; }        
    }
}