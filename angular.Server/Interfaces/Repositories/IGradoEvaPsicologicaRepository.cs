using Entities;
using Interfaces.Repositories.Base;

namespace Interfaces.Repositories
{
    public interface IGradoEvaPsicologicaRepository : IRepository<GradoEvaPsicologica>
    {
        Task<int?> GetTestPsicologicoIdPorGrado(int aulaId);
    }
}
