using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNSirindar.Models;
using SirindarApiService.AppModels;

namespace SirindarApiService
{
    public interface ISirindarApi
    {
        Task<bool> LogIn(LoginModel model);
        Task<Deportista> GetDeportista(int matricula);
        Task<IEnumerable<Horario>> Horarios();
        Task<AsistenciaResultado> RegistrarAsistencia(Asistencia asistencia);
    }
}
