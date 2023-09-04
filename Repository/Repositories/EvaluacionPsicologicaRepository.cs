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
    public class EvaluacionPsicologicaRepository: Repository<EvaluacionPsicologica>, IEvaluacionPsicologicaRepository
    {
        public EvaluacionPsicologicaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<IList<EscalaPsicologica>?> ResultadosPsicologicosAula(int evaPsiAulaId, int evaPsiId, int dimensionId)
        {
            var EvalucionPsicologica = await Table
                   .Include(eva => eva!.DimensionesPsicologicas!.Where(d => d.Id == dimensionId))
                   .ThenInclude(dim => dim!.EscalasPsicologicas!)
                   .ThenInclude(esc => esc!.IndicadoresPsicologicos!)
                   .ThenInclude(ind => ind!.PreguntasPsicologicas!)
                   .ThenInclude(preg => preg!.RespuestasPsicologicas!)
                   .ThenInclude(resp => resp!.EvaluacionPsicologicaEstudiante!)
                   .FirstOrDefaultAsync(eva => eva.Id == evaPsiId);

                var escalasPsicologicas = EvalucionPsicologica?.DimensionesPsicologicas?.FirstOrDefault()?.EscalasPsicologicas;

                if (escalasPsicologicas != null)
                    return  escalasPsicologicas
                        .Where(esc => esc!.IndicadoresPsicologicos!
                            .SelectMany(ind => ind!.PreguntasPsicologicas!)
                            .SelectMany(preg => preg!.RespuestasPsicologicas!)
                            .Select(resp => resp!.EvaluacionPsicologicaEstudiante!)
                            .Any(resp => resp.EvaluacionAulaId== evaPsiAulaId))
                        .ToList();

            return escalasPsicologicas;
        }

        public async Task<IList<EscalaPsicologica>?> ResultadosPsicologicosEstudiante(int evaPsiEstId, int evaPsiId, int dimensionId)
        {
            var respuestasEscalasPsicologicas = await Table
                .Include(x => x!.DimensionesPsicologicas!.Where(r => r.Id == dimensionId))
                .ThenInclude(x => x!.EscalasPsicologicas!)
                .ThenInclude(x => x!.IndicadoresPsicologicos!)
                .ThenInclude(x => x!.PreguntasPsicologicas!)
                .ThenInclude(x => x!.RespuestasPsicologicas!.Where(r => r.EvaPsiEstId == evaPsiEstId))
                .Where(e => e.Id == evaPsiId && e!.DimensionesPsicologicas!.Any(d => d.Id == dimensionId))
                .ToListAsync();

            return respuestasEscalasPsicologicas?.FirstOrDefault()?.DimensionesPsicologicas?.FirstOrDefault()?.EscalasPsicologicas;
        }
    }
}
