using Contracts.Repositories;
using Domain.Entities;
using Repository.Context;
using Repository.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UsuarioRepository: Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository (ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public Task<Usuario?> ObtenerUsuario(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
