using Contracts.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IEstudianteAulaRepository : IRepository<EstudianteAula>
    {
        Task<int?> AulaIdActualEstudiante(int estudianteId);
        Task<Aula?> AulaActualEstudiante (int estudianteId);
        Task<int?> AulaIdPorEstudianteIdYAnio(int estudianteId, int anio);
        Task<Aula?> AulaPorEstudianteIdYAnio(int estudianteId, int anio);

    }
}
