using Entities;
using Interfaces.Repositories.Base;

namespace Interfaces.Repositories
{
    public interface IIndicadorPsicologicoRepository : IRepository<IndicadorPsicologico>
    {
        Task<double?> PromedioIndicadorPsicologicoEstudiante(int evaPsiEstId, int indicadorId);
    }
}
