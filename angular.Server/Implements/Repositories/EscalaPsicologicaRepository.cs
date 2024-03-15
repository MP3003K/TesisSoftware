using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Context;

namespace Implements.Repositories
{
    public class EscalaPsicologicaRepository: Repository<EscalaPsicologica>, IEscalaPsicologicaRepository
    {
        public EscalaPsicologicaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<IList<EscalaPsicologica>?> ObtenerEscalaPorDimensionId(int evaPsiId, int dimensionId)
        {
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
