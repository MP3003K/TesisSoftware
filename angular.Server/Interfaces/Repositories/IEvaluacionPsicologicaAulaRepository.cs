using Entities;
using Interfaces.Repositories.Base;

namespace Interfaces.Repositories
{
    public interface IEvaluacionPsicologicaAulaRepository : IRepository<EvaluacionPsicologicaAula>
    {
        Task<EvaluacionPsicologicaAula?> EvaPsiAulaPorAulaIdYUnidadId(int aulaId, int unidadId);
        Task<int?> EvaPsiAulaIdPorAulaIdYUnidadId(int aulaId, int unidadId);
        Task<EvaluacionPsicologicaAula?> EvaPsiAulaIncluyendoListaEstudiante(int aulaId, int unidadId);
    }
}
