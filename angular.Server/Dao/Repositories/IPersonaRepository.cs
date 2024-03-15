using Domain.Entities;
using webapi.Dao.Repositories.Base;

namespace webapi.Dao.Repositories
{
    public interface IPersonaRepository : IRepository<Persona>
    {
        Task<Estudiante?> ObtenerEstudiantePorPersonaId(int personaId);
    }
}
