﻿using Contracts.Repositories;
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
    public class EstudianteAulaRepository: Repository<EstudianteAula>, IEstudianteAulaRepository
    {
        public EstudianteAulaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<int?> AulaIdPorEstudianteId(int estudianteId)
        {
            var aulaId = await Table
                .Where(ea => ea.EstudianteId == estudianteId && ea.Estado == "1")
                .Select(ea => ea.AulaId)
                .FirstOrDefaultAsync();

            return aulaId;
        }
    }
}
