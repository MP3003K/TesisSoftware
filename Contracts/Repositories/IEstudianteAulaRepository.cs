using Contracts.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IEstudianteAulaRepository : IRepository<EstudianteAula>
    {
        Task<int?> AulaIdPorEstudianteId (int estudianteId);

    }
}
