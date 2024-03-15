using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
{
    public class GradoEvaPsicologicaRepository: Repository<GradoEvaPsicologica>, IGradoEvaPsicologicaRepository
    {
        public GradoEvaPsicologicaRepository (ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<int?> GetTestPsicologicoIdPorGrado(int gradoId)
        {
            return await Table
                .Where(gep => gep.GradoId == gradoId)
                .Select(gep => gep.Id)
                .FirstOrDefaultAsync();
        }
    }
}
