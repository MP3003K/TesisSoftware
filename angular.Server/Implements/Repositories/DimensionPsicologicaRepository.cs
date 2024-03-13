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
    public class DimensionPsicologicaRepository: Repository<DimensionPsicologica>, IDimensionPsicologicaRepository
    {
        public DimensionPsicologicaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
