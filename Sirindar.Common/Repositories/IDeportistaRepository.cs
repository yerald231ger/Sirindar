using System.Collections.Generic;

namespace Sirindar.Core.Repositories
{
    public interface IDeportistaRepository : IRepository<Deportista, int>
    {
        Deportista GetByMatricula(string matricula);
        HorarioComidas GetHorarioComidas(int deportistaId);
        Deportista GetWithDeportes(int deportistaId);

        IEnumerable<Deportista> SearchByMatricula(string matricula);
        IEnumerable<Deportista> SearchByStringExpression(string expression);
        IEnumerable<Deportista> SearchByDeporte(string deporte);
        IEnumerable<Deportista> SearchByDependencia(string deporte);
    }
}