using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Context;


namespace Implements.Repositories
{
    public class AulaRepository: Repository<Aula>, IAulaRepository 
    {
        public AulaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<IList<Aula>> ObtenerTodasLasAulas()
        {
            var aulas = await Table
                           .Include(a => a.Grado) // Incluir la relación con Grado
                           .OrderBy(a => a.GradoId)
                           .ThenBy(a => a.Secccion)
                           .ToListAsync();

            return aulas;
        }

    }
}
