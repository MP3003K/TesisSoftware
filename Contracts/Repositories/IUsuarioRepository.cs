using Contracts.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IUsuarioRepository: IRepository<Usuario>
    {
        Task<Usuario?> ObtenerUsuario(string username, string password);
    }
}
