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
    public class IndicadorPsicologicoRepository: Repository<IndicadorPsicologico>, IIndicadorPsicologicoRepository
    {
        public IndicadorPsicologicoRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<double?> PromedioIndicadorPsicologicoEstudiante(int evaPsiEstId, int indicadorId)
        {
            var promedioIndicador = await Table
                .Where(i => i.Id == indicadorId)
                .SelectMany(i => i.PreguntasPsicologicas.SelectMany(p => p.RespuestasPsicologicas))
                .Where(r => r.EvaPsiEstId == evaPsiEstId)
                .AverageAsync(r => Convert.ToDouble(r.Respuesta));

            return Math.Round(promedioIndicador, 4); // Redondear a 2 decimales
        }
        
    }
}
