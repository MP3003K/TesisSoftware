using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Dao.Repositories.Base;

namespace webapi.Dao.Repositories
{
    public interface IPreguntaPsicologicaRepository : IRepository<PreguntaPsicologica>
    {
        Task<IList<PreguntaPsicologica>?> PreguntaPsicologicasPaginadas(int evaPsiId, int evaPsiEstId, int pageSize, int pageNumber);
    }
}
