using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
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
