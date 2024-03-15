using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Context;

namespace Implements.Repositories
{
    public class PreguntaPsicologicaRepository : Repository<PreguntaPsicologica>, IPreguntaPsicologicaRepository
    {
        public PreguntaPsicologicaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<IList<PreguntaPsicologica>?> PreguntaPsicologicasPaginadas(int evaPsiId, int evaPsiEstId, int pageSize, int pageNumber)
        {

            var preguntaPsicologica = await Table
                .Where(pp => pp.IndicadorPsicologico!.EscalaPsicologica!.DimensionPsicologica!.EvaluacionPsicologica!.Id == evaPsiId)
                .Include(pp => pp.RespuestasPsicologicas!.Where(r => r.EvaPsiEstId == evaPsiEstId))
                .ThenInclude(r => r.EvaluacionPsicologicaEstudiante)
                .OrderBy(pp => pp.NPregunta)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return preguntaPsicologica;
        }
    }
}
