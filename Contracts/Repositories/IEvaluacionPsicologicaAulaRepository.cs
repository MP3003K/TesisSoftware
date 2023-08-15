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
        Task<EvaluacionPsicologicaAula?> EvaPsiAulaPorAulaIdYUnidadId(int aulaId, int unidadId);
        Task<int?> EvaPsiAulaIdPorAulaIdYUnidadId(int aulaId, int unidadId);
    }
}
