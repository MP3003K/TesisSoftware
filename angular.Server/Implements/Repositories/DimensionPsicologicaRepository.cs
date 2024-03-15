using Domain.Entities;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
{
    public class DimensionPsicologicaRepository: Repository<DimensionPsicologica>, IDimensionPsicologicaRepository
    {
        public DimensionPsicologicaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
