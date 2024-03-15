using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Context;

namespace Implements.Repositories
{
    public class RolRepository: Repository<Rol>, IRolRepository
    {
        public RolRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
