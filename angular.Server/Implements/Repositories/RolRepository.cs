using Domain.Entities;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
{
    public class RolRepository: Repository<Rol>, IRolRepository
    {
        public RolRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
