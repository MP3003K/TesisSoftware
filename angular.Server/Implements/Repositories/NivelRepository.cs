using Domain.Entities;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
{
    public class NivelRepository: Repository<Nivel>, INivelRepository
    {
        public NivelRepository (ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
