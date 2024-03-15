using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Context;

namespace Implements.Repositories
{
    public class EvaluacionPsicologicaEstudianteRepository : Repository<EvaluacionPsicologicaEstudiante>, IEvaluacionPsicologicaEstudianteRepository
    {
        public EvaluacionPsicologicaEstudianteRepository (ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<int?> EvaPsiEstudianteIdPorEstudianteId(int evaPsiAulaId, int estudianteId)
        {
            return await Table
                .Where(epe => epe.EvaluacionAulaId == evaPsiAulaId && epe.EstudianteId == estudianteId)
                .Select(epe => epe.Id)
                .FirstOrDefaultAsync();
        }
        public async Task<EvaluacionPsicologicaEstudiante?> EvaPsiEstudiante(int estudianteId, int unidadId, int aulaId)
        {
            var evaPsiEstudiante = await Table
                .Include(x => x.EvaluacionAula)
                .ThenInclude(x => x.EvaluacionPsicologica)
                .Where(x => x.EstudianteId == estudianteId && 
                x.EvaluacionAula != null && 
                x.EvaluacionAula.UnidadId == unidadId &&
                x.EvaluacionAula.AulaId == aulaId && 
                x.EvaluacionAula.EvaluacionPsicologica != null)
                .FirstOrDefaultAsync();

            return evaPsiEstudiante;
        }

        public async Task<IList<Aula>?> ListaAulasDeEvaPsiEstPorIdEstudiante(int estudianteId)
        {
            var aulas = await Table
                .Where(epe => epe.EstudianteId == estudianteId)
                .Include(epe => epe.EvaluacionAula)
                .ThenInclude(ea => ea.Aula)
                .ThenInclude(a => a.Grado)
                .Where(epe => epe.EvaluacionAula.AulaId == epe.EvaluacionAula.Aula.Id)
                .Select(epe => epe.EvaluacionAula.Aula)
                .ToListAsync();

            return aulas;
        }

        public async Task<IList<Unidad>?> ListaUnidadesDeEvaPsiEstPorIdEstudiante(int estudianteId)
        {
            var unidades = await Table
                .Where(epe => epe.EstudianteId == estudianteId)
                .Include(epa => epa.EvaluacionAula)
                .ThenInclude(u => u.Unidad)
                .Select(epa => epa.EvaluacionAula.Unidad)
                .ToListAsync();

            return unidades;
        }

        public async Task<EvaluacionPsicologicaAula?> EvaPsiAulaPorEvaPsiEstudianteId(int evaPsiEstId)
        {
            var EvaPsiAula = await Table
                .Where(epe => epe.Id == evaPsiEstId)
                .Select(epe => epe.EvaluacionAula)
                .FirstOrDefaultAsync();

            return EvaPsiAula;
        }

        public Task<bool> VerificarTestPsicologicoCompleto(int evaPsiEstId)
        {
            var estadoTestPsicologico = Table
                .Where(epe => epe.Id == evaPsiEstId)
                .Select(epe => epe.Estado)
                .FirstOrDefault();
            if (!string.IsNullOrEmpty(estadoTestPsicologico) && estadoTestPsicologico == "F")
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }


    }
}
