using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Context;

namespace Implements.Repositories
{
    public class IndicadorPsicologicoRepository: Repository<IndicadorPsicologico>, IIndicadorPsicologicoRepository
    {
        public IndicadorPsicologicoRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<double?> PromedioIndicadorPsicologicoEstudiante(int evaPsiEstId, int indicadorId)
        {
            var promedioIndicador = await Table
                .Where(i => i.Id == indicadorId)
                .SelectMany(i => i.PreguntasPsicologicas.SelectMany(p => p.RespuestasPsicologicas))
                .Where(r => r.EvaPsiEstId == evaPsiEstId)
                .AverageAsync(r => Convert.ToDouble(r.Respuesta));

            return Math.Round(promedioIndicador, 4); // Redondear a 2 decimales
        }
        
    }
}
