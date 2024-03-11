using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Dao.Repositories.Base;

namespace webapi.Dao.Repositories
{
    public interface IAulaRepository : IRepository<Aula>
    {
        Task<IList<Aula>> ObtenerTodasLasAulas();
    }
}
