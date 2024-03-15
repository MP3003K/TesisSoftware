using Entities;
using Interfaces.Repositories.Base;

namespace Interfaces.Repositories
{
    public interface IEvaluacionPsicologicaRepository : IRepository<EvaluacionPsicologica>
    {
        Task<IList<EscalaPsicologica>?> ResultadosPsicologicosEstudiante(int evaPsiEstId, int evaPsiId, int dimensionId);
        Task<IList<EscalaPsicologica>?> ResultadosPsicologicosAula(int evaPsiAulaId, int evaPsiId, int dimensionId);
    }
}
