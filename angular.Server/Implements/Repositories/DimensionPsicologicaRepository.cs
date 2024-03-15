using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Context;

namespace Implements.Repositories
{
    public class DimensionPsicologicaRepository: Repository<DimensionPsicologica>, IDimensionPsicologicaRepository
    {
        public DimensionPsicologicaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
