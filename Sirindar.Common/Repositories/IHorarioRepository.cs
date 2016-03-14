using System;

namespace Sirindar.Core.Repositories
{
    public interface IHorarioRepository : IRepository<Horario, int>
    {
        Horario GetHorarioBetweenHour(DateTime dateTime);
    }
}