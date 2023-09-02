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
    public class EvaluacionPsicologicaAulaRepository: Repository<EvaluacionPsicologicaAula>, IEvaluacionPsicologicaAulaRepository
    {
        public EvaluacionPsicologicaAulaRepository (ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<EvaluacionPsicologicaAula?> EvaPsiAulaPorAulaIdYUnidadId(int aulaId, int unidadId)
        {
            var evaPsiAula = await Table
                .Include(epa => epa.EvaluacionPsicologica)
                .ThenInclude(x => x.DimensionesPsicologicas)
                .Where(epa => epa.AulaId == aulaId && epa.UnidadId == unidadId)
                .FirstOrDefaultAsync();

            return evaPsiAula;

        }

        public async Task<int?> EvaPsiAulaIdPorAulaIdYUnidadId(int aulaId, int unidadId)
        {
            var evaluacionesAulaIds = await Table
                .Where(epa => epa.AulaId == aulaId && epa.UnidadId == unidadId)
                .Select(epa => epa.Id)
                .FirstOrDefaultAsync();

            return evaluacionesAulaIds;

        }

        public async Task<EvaluacionPsicologicaAula?> EvaPsiAulaIncluyendoListaEstudiante(int aulaId, int unidadId)
        {
            var listaEstudiantes = await Table
                .Where(epa => epa.AulaId == aulaId && epa.UnidadId == unidadId
                && epa.EvaluacionesPsicologicasEstudiante != null)
                .Include(x => x.EvaluacionesPsicologicasEstudiante).ThenInclude(x => x.Estudiante).ThenInclude(x => x.Persona)
                .FirstAsync();

            return listaEstudiantes;
        }

    }
}
