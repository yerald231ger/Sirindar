namespace Sirindar.Core.Repositories
{
    public interface IDeporteDeportistaRepository : IRepository<DeporteDeportista, int>
    {
        DeporteDeportista FindByDeporteId(int deporteId);
    }
}