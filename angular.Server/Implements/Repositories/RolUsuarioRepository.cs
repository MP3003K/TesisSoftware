using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Context;

namespace Implements.Repositories
{
    public class RolUsuarioRepository: Repository<RolUsuario> , IRolUsuarioRepository
    {
        public RolUsuarioRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
