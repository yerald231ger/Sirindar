using System.Collections.Generic;
using CNSirindar.Models;
using ServiciosCafeteria.AppModels;

namespace ServiciosCafeteria.Interfaces
{
    public interface IImpresora
    {
        void Imprimir(IList<Equipo> ticket);
    }
}
