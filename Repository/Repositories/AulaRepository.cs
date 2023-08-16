using Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repository.Context;
using Repository.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AulaRepository: Repository<Aula>, IAulaRepository
    {
        public AulaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<IList<Aula>> ObtenerTodasLasAulas()
        {
            var aulas = await Table
                           .Include(a => a.Grado) // Incluir la relación con Grado
                           .OrderBy(a => a.GradoId)
                           .ThenBy(a => a.Secccion)
                           .ToListAsync();

            return aulas;
        }

    }
}
