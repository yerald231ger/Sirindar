using System;
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
