using Contracts.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IEscalaPsicologicaRepository: IRepository<EscalaPsicologica>
    {
        Task<IList<EscalaPsicologica>?> ObtenerEscalaPorDimensionId(int evaPsiId, int dimensionId); 
    }
}
