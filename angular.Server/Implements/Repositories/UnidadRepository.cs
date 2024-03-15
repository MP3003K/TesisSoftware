using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
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
