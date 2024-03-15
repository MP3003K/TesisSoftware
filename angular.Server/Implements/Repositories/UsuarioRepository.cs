using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
{
    public class UsuarioRepository: Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository (ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<Usuario?> ObtenerUsuario(string username, string password)
        {
            var usuario = await Table
                .Include(u => u.RolesUsuarios).ThenInclude(ru => ru.Rol)
                .Include(u => u.Persona)
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();

            return usuario;
        }
    }
}
