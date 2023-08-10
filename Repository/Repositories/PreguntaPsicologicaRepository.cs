using Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{

    public class PreguntaPsicologicaRepository : Repository<PreguntaPsicologica>, IPreguntaPsicologicaRepository
    {
        public PreguntaPsicologicaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
