using Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            .Where(u => u.Año == 2023 && u.NUnidad == 8)
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
