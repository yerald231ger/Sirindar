using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNSirindar.Models;

namespace SirindarApiService
{
    interface ISirindarApi
    {
        bool LogIn();
        Deportista GetDeportista(int Id);
        IEnumerable<Horario> Horarios();
        bool LimiteComidas();
        bool VerificarAsistencia();
        bool RegistrarAsistencia();
    }
}
