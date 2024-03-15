using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
{
    public class RespuestaPsicologicaRepository: Repository<RespuestaPsicologica>, IRespuestaPsicologicaRepository
    {
        public RespuestaPsicologicaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<IList<RespuestaPsicologica>> GetAllRespuestasPsicologicas(int evaPsiEstId, int pageNumber, int pageSize)
        {
            var respuestaPsicologica = await Table.Where(x => x.EvaPsiEstId == evaPsiEstId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return respuestaPsicologica;
        }

        public async Task<RespuestaPsicologica?> GetRespuestaPsicologica(int evaPsiEstId, int preguntaId)
        {
            var respuesta = await Table.Where(x => x.EvaPsiEstId == evaPsiEstId && x.PreguntaPsicologicaId == preguntaId)
                .FirstOrDefaultAsync();

            return respuesta;
        }

        public async Task<string?> GetRespuestaDeUnaPreguntaPorEvaPsiEstIdYPreguntaId(int evaPsiEstId, int preguntaId)
        {
            var respuesta = await Table.Where(x => x.EvaPsiEstId == evaPsiEstId && x.PreguntaPsicologicaId == preguntaId)
                .Select(x => x.Respuesta)
                .FirstOrDefaultAsync();

                return respuesta;
        }

        public async Task<double?> PromedioRespuestasIndicadorEnAula(int aulaId, int indicadorId)
        {
            var promedioRespuestas = await Table
                .Where(rp => rp.PreguntaPsicologica.IndicadorPsicologicoId == indicadorId &&
                             rp.EvaluacionPsicologicaEstudiante.EvaluacionAula.AulaId == aulaId)
                .Select(rp => Convert.ToDouble(rp.Respuesta))
                .AverageAsync();

            return Math.Round(promedioRespuestas, 4);
        }
    }
}
