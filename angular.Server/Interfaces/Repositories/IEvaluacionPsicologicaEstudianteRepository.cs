using Entities;
using Interfaces.Repositories.Base;

namespace Interfaces.Repositories
{
    public interface IEvaluacionPsicologicaEstudianteRepository : IRepository<EvaluacionPsicologicaEstudiante>
    {
        Task<int?> EvaPsiEstudianteIdPorEstudianteId(int evaPsiAulaId, int estudianteId);
        Task<EvaluacionPsicologicaEstudiante?> EvaPsiEstudiante(int estudianteId, int unidadId, int aulaId);
        Task<IList<Unidad>?> ListaUnidadesDeEvaPsiEstPorIdEstudiante(int estudianteIds);
        Task<IList<Aula>?> ListaAulasDeEvaPsiEstPorIdEstudiante(int estudianteId);
        Task<EvaluacionPsicologicaAula?> EvaPsiAulaPorEvaPsiEstudianteId(int evaPsiEstId);
        Task<bool> VerificarTestPsicologicoCompleto(int evaPsiEstId);
    }
}
