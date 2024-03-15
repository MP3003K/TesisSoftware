using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Context;

namespace Implements.Repositories
{
    public class RolAccesoRepository: Repository<RolAcceso>, IRolAccesoRepository
    {
        public RolAccesoRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
