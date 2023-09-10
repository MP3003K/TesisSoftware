using Contracts.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IEvaluacionPsicologicaRepository: IRepository<EvaluacionPsicologica>
    {
        Task<IList<EscalaPsicologica>?> ResultadosPsicologicosEstudiante(int evaPsiEstId, int evaPsiId, int dimensionId);
        Task<IList<EscalaPsicologica>?> ResultadosPsicologicosAula(int evaPsiAulaId, int evaPsiId, int dimensionId);
    }
}
