using Contracts.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IIndicadorPsicologicoRepository: IRepository<IndicadorPsicologico>
    {
        Task<double?> PromedioIndicadorPsicologicoEstudiante(int evaPsiEstId, int indicadorId);
    }
}
