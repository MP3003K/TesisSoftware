using Entities;
using Interfaces.Repositories.Base;

namespace Interfaces.Repositories
{
    public interface IEscalaPsicologicaRepository : IRepository<EscalaPsicologica>
    {
        Task<IList<EscalaPsicologica>?> ObtenerEscalaPorDimensionId(int evaPsiId, int dimensionId);
    }
}
