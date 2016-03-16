using System;
using System.Collections.Generic;

namespace Sirindar.Core.Repositories
{
    public interface IDeportistaRepository : IRepository<Deportista, int>
    {
        Deportista GetByMatricula(string matricula);
        HorarioComidas GetHorarioComidas(int deportistaId);
        Deportista GetWithDeportes(int deportistaId);

        IEnumerable<Deportista> GetAllByExpression(string expression);
        IEnumerable<Deportista> SearchByMatricula(string matricula);
        IEnumerable<Deportista> SearchByStringExpression(string expression);
        IEnumerable<Deportista> SearchByDeporte(string deporte);
        IEnumerable<Deportista> SearchByDependencia(string deporte);
        bool IsMatricula(string matricula);
        bool IsDeportista(int deportistaId);
        void AddDeporte(int deportistaId, int deporteId, TimeSpan iniciaEntrenamiento, TimeSpan finalizaEntrenamiento);
        void RemoveDeporte(int deportistaId, int deporteId);
    }
}