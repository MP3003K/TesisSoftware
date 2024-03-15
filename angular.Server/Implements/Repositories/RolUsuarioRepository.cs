using Domain.Entities;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
{
    public class RolUsuarioRepository: Repository<RolUsuario> , IRolUsuarioRepository
    {
        public RolUsuarioRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
