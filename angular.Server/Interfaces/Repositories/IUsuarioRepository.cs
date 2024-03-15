using Entities;
using Interfaces.Repositories.Base;

namespace Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> ObtenerUsuario(string username, string password);
    }
}
