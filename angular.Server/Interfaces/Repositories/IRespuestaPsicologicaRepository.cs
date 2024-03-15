using Entities;
using Interfaces.Repositories.Base;

namespace Interfaces.Repositories
{
    public interface IRespuestaPsicologicaRepository : IRepository<RespuestaPsicologica>
    {
        Task<IList<RespuestaPsicologica>> GetAllRespuestasPsicologicas(int evaPsiEstId, int pageNumber, int pageSize);
        Task<RespuestaPsicologica?> GetRespuestaPsicologica(int evaPsiEstId, int preguntaId);
        Task<string?> GetRespuestaDeUnaPreguntaPorEvaPsiEstIdYPreguntaId(int evaPsiEstId, int pregunta);
        Task<double?> PromedioRespuestasIndicadorEnAula(int aulaId, int indicadorId);
    }
}
