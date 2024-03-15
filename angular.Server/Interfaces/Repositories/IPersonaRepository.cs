using Entities;
using Interfaces.Repositories.Base;

namespace Interfaces.Repositories
{
    public interface IPersonaRepository : IRepository<Persona>
    {
        Task<Estudiante?> ObtenerEstudiantePorPersonaId(int personaId);
    }
}
