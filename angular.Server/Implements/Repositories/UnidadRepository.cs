using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Context;

namespace Implements.Repositories
{
    public class UnidadRepository : Repository<Unidad>, IUnidadRepository
    {
        public UnidadRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<Unidad?> ObtenerUnidadPorAnioYNUnidad(int anio, int nUnidad)
        {
            var unidad = await Table
                            .Where(u => u.Año == anio && u.NUnidad == nUnidad)
                            .FirstOrDefaultAsync();

            return unidad;
        }

        public async Task<Unidad?> UnidadActual()
        {
            /* Cuando el estado es "O" = "Operativa"*/
            return await Table.Where(u => u.Estado == "O").FirstOrDefaultAsync();
        }


    }
}
