using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Context;

namespace Implements.Repositories
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
