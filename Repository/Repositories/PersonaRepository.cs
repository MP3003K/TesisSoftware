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
    public class PersonaRepository: Repository<Persona>, IPersonaRepository
    {
        public PersonaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<Estudiante?> ObtenerEstudiantePorPersonaId(int personaId)
        {
            var estudiante = await Table
                .Include(p => p.Estudiante)
                .ThenInclude(e => e!.EstudiantesAulas!.Where(a => a.Estado == "1"))
                .ThenInclude(ea => ea.Aula)
                .ThenInclude(a => a!.Grado)
                .Where(p => p.Id == personaId)
                .Select(p => p.Estudiante)
                .FirstOrDefaultAsync();

            return estudiante;
        }
    }
}
