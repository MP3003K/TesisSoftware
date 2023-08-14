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
    public class GradoEvaPsicologicaRepository: Repository<GradoEvaPsicologica>, IGradoEvaPsicologicaRepository
    {
        public GradoEvaPsicologicaRepository (ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<int?> GetTestPsicologicoIdPorGrado(int gradoId)
        {
            return await Table
                .Where(gep => gep.GradoId == gradoId)
                .Select(gep => gep.Id)
                .FirstOrDefaultAsync();
        }
    }
}
