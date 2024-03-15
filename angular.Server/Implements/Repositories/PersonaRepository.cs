﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

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
