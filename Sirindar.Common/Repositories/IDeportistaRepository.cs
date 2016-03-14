namespace Sirindar.Core.Repositories
{
    public interface IDeportistaRepository : IRepository<Deportista, int>
    {
        Deportista GetDeportistaByMatricula(string matricula);
    }
}