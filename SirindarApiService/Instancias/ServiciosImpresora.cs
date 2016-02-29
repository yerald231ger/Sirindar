using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiciosCafeteria.Interfaces;

namespace ServiciosCafeteria.Instancias
{
    public class Impresora : IImpresora
    {
        public void Imprimir<T>(T reporte)
        {
            
        }
    }

    public class ImpresoraTest : IImpresora 
    {
        public void Imprimir<T>(T reporte)
        {
            throw new NotImplementedException("Imprimiendo Ticket");
        }
    }
}
