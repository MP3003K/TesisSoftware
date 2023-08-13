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
    public class RespuestaPsicologicaRepository: Repository<RespuestaPsicologica>, IRespuestaPsicologicaRepository
    {
        public RespuestaPsicologicaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<IList<RespuestaPsicologica>> ObtenerRespuestasDeEstudiante(int evaPsiEstId, int pageNumber, int pageSize)
        {
            var respuestaPsicologica = await Table.Where(x => x.EvaPsiEstId == evaPsiEstId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return respuestaPsicologica;
        }

        public async Task<string?> RespuestaDeUnaPregunta(int evaPsiEstId, int preguntaId)
        {
            var respuesta = await Table.Where(x => x.EvaPsiEstId == evaPsiEstId && x.PreguntaPsicologicaId == preguntaId)
                .Select(x => x.Respuesta)
                .FirstOrDefaultAsync();

                return respuesta;
        }
    }
}
