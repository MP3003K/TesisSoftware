using Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EvaluacionPsicologicaAulaRepository: Repository<EvaluacionPsicologicaAula>, IEvaluacionPsicologicaAulaRepository
    {
        public EvaluacionPsicologicaAulaRepository (ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<int?> EvaluacionPsicologicaAulaIdPorAulaId(int aulaId, int unidadId, int evaPsiId)
        {
            var Id = await Table
                .Where(epa => epa.AulaId == aulaId && epa.UnidadId == unidadId && epa.EvaluacionPsicologicaId == evaPsiId)
                .Select(epa => epa.Id)
                .FirstOrDefaultAsync();
            return Id;
        }
    }
}
