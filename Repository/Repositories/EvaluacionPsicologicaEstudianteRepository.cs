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
    public class EvaluacionPsicologicaEstudianteRepository : Repository<EvaluacionPsicologicaEstudiante>, IEvaluacionPsicologicaEstudianteRepository
    {
        public EvaluacionPsicologicaEstudianteRepository (ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<int?> EvalucionPsicologicaEstudianteIdPorEstudianteId(int evaPsiAulaId, int estudianteId)
        {
            return await Table
                .Where(epe => epe.EvaluacionAulaId == evaPsiAulaId && epe.EstudianteId == estudianteId)
                .Select(epe => epe.Id)
                .FirstOrDefaultAsync();
        }
    }
}
