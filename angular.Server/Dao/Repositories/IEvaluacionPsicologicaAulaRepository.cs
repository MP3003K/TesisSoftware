using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Dao.Repositories.Base;

namespace webapi.Dao.Repositories
{
    public interface IEvaluacionPsicologicaAulaRepository : IRepository<EvaluacionPsicologicaAula>
    {
        Task<EvaluacionPsicologicaAula?> EvaPsiAulaPorAulaIdYUnidadId(int aulaId, int unidadId);
        Task<int?> EvaPsiAulaIdPorAulaIdYUnidadId(int aulaId, int unidadId);
        Task<EvaluacionPsicologicaAula?> EvaPsiAulaIncluyendoListaEstudiante(int aulaId, int unidadId);
    }
}
