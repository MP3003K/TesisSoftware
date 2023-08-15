using Contracts.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IEvaluacionPsicologicaAulaRepository: IRepository<EvaluacionPsicologicaAula>
    {
        Task<int?> EvaluacionPsicologicaAulaIdPorAulaId(int aulaId, int unidadId, int evaPsiId);
    }
}
