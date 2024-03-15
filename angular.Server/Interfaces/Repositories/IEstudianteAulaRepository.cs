using Entities;
using Interfaces.Repositories.Base;

namespace Interfaces.Repositories
{
    public interface IEstudianteAulaRepository : IRepository<EstudianteAula>
    {
        Task<int?> AulaIdActualEstudiante(int estudianteId);
        Task<Aula?> AulaActualEstudiante(int estudianteId);
        Task<int?> AulaIdPorEstudianteIdYAnio(int estudianteId, int anio);
        Task<Aula?> AulaPorEstudianteIdYAnio(int estudianteId, int anio);
    }
}
