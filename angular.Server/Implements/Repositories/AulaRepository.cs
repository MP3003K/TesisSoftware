using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
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
