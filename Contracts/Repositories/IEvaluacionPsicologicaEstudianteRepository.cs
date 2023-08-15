﻿using Contracts.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IEvaluacionPsicologicaEstudianteRepository: IRepository<EvaluacionPsicologicaEstudiante>
    {
        Task<int?> EvaPsiEstudianteIdPorEstudianteId(int evaPsiAulaId, int estudianteId);
    }
}
