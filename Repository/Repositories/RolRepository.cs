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
    public class RolRepository: Repository<Rol>, IRolRepository
    {
        public RolRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
