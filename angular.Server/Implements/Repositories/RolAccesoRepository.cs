using Domain.Entities;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
{
    public class RolAccesoRepository: Repository<RolAcceso>, IRolAccesoRepository
    {
        public RolAccesoRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
