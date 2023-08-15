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
        Task<IList<RespuestaPsicologica>> ObtenerRespuestasDeEstudiante(int evaPsiEstId, int pageNumber, int pageSize);
        Task<string?> RespuestaDeUnaPregunta(int evaPsiEstId, int pregunta);
    }
}
