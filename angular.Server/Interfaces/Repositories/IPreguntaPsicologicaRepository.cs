using Entities;
using Interfaces.Repositories.Base;

namespace Interfaces.Repositories
{
    public interface IPreguntaPsicologicaRepository : IRepository<PreguntaPsicologica>
    {
        Task<IList<PreguntaPsicologica>?> PreguntaPsicologicasPaginadas(int evaPsiId, int evaPsiEstId, int pageSize, int pageNumber);
    }
}
