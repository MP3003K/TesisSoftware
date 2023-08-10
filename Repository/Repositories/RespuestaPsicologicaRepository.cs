using Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;

namespace Repository.Repositories
{
    public class RespuestaPsicologicaRepository : Repository<RespuestaPsicologica>, IRespuestaPsicologicaRepository 
    {
        public RespuestaPsicologicaRepository(ApplicationDbContext dBContext) : base(dBContext)  
        {
        }

        public async Task<RespuestaPsicologica?> ObtenerRespuestasPsicologicasPorEstudianteId(int estudianteId)
        {
            return await Table.FirstOrDefaultAsync(x => x.Id == estudianteId);
        }
    }
}
