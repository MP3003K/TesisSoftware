using Contracts.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IRespuestaPsicologicaRepository: IRepository<RespuestaPsicologica>
    {
        Task<IList<RespuestaPsicologica>> GetAllRespuestasPsicologicas(int evaPsiEstId, int pageNumber, int pageSize);
        Task<RespuestaPsicologica?> GetRespuestaPsicologica(int evaPsiEstId, int preguntaId);
        Task<string?> GetRespuestaDeUnaPreguntaPorEvaPsiEstIdYPreguntaId(int evaPsiEstId, int pregunta);
        Task<double?> PromedioRespuestasIndicadorEnAula(int aulaId, int indicadorId);
    }
}
