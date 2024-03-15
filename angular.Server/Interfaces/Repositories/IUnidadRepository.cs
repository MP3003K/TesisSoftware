using Entities;
using Interfaces.Repositories.Base;

namespace Interfaces.Repositories
{
    public interface IUnidadRepository : IRepository<Unidad>
    {
        Task<Unidad?> UnidadActual();
        Task<Unidad?> ObtenerUnidadPorAnioYNUnidad(int anio, int nUnidad);
    }
}
