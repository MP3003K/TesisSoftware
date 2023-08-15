using Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repository.Context;
using Repository.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EscalaPsicologicaRepository: Repository<EscalaPsicologica>, IEscalaPsicologicaRepository
    {
        public EscalaPsicologicaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<IList<EscalaPsicologica>?> ObtenerEscalaPorDimensionId(int evaPsiId, int dimensionId)
        {
            /*
            var escalasPsicologicas = await Table
                .Include(ep => ep.DimensionPsicologica)
                    .ThenInclude(dp => dp.EvaluacionPsicologica)
                .Where(ep => ep.DimensionPsicologica.EvaluacionPsicologica.Id == evaPsiId)
                .ToListAsync();
            */

            /*var escalasPsicologicas = await Table
              .Include(ep => ep.DimensionPsicologica)
                  .ThenInclude(dp => dp.EvaluacionPsicologica)
              .Include(ep => ep.IndicadoresPsicologicos) // Incluir indicadores relacionados
              .Where(ep => ep.DimensionPsicologica.EvaluacionPsicologica.Id == evaPsiId)
              .ToListAsync();
            */


            var escalas = await Table
                .Include(ep => ep.DimensionPsicologica)
                    .ThenInclude(dp => dp.EvaluacionPsicologica)
                .Include(ep => ep.IndicadoresPsicologicos) // Incluir indicadores relacionados
                .Where(ep => ep.DimensionPsicologica.EvaluacionPsicologica.Id == evaPsiId && ep.DimensionId == dimensionId)
                .ToListAsync();


            return escalas;
        }
    }
}
