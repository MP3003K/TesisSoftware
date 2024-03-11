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
    public class RolUsuarioRepository: Repository<RolUsuario> , IRolUsuarioRepository
    {
        public RolUsuarioRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
