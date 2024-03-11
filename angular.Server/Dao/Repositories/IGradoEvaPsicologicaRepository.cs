using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Dao.Repositories.Base;

namespace webapi.Dao.Repositories
{
    public interface IGradoEvaPsicologicaRepository : IRepository<GradoEvaPsicologica>
    {
        Task<int?> GetTestPsicologicoIdPorGrado(int aulaId);
    }
}
