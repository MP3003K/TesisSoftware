﻿using Contracts.Repositories;
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
    public class EvaluacionPsicologicaAulaRepository: Repository<EvaluacionPsicologicaAula>, IEvaluacionPsicologicaAulaRepository
    {
        public EvaluacionPsicologicaAulaRepository (ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<EvaluacionPsicologicaAula?> EvaPsiAulaPorAulaIdYUnidadId(int aulaId, int unidadId)
        {
            var evaPsiAula = await Table
                .Where(epa => epa.AulaId == aulaId && epa.UnidadId == unidadId)
                .FirstOrDefaultAsync();

            return evaPsiAula;

        }

        public async Task<int?> EvaPsiAulaIdPorAulaIdYUnidadId(int aulaId, int unidadId)
        {
            var evaluacionesAulaIds = await Table
                .Where(epa => epa.AulaId == 1 && epa.UnidadId == 1)
                .Select(epa => epa.Id)
                .FirstOrDefaultAsync();

            return evaluacionesAulaIds;

        }
    }
}
