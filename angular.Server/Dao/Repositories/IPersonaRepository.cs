using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Dao.Repositories.Base;

namespace webapi.Dao.Repositories
{
    public interface IPersonaRepository : IRepository<Persona>
    {
        Task<Estudiante?> ObtenerEstudiantePorPersonaId(int personaId);
    }
}
