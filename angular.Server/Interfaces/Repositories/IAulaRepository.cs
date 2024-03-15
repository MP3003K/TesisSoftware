using Entities;
using Interfaces.Repositories.Base;

namespace Interfaces.Repositories
{
    public interface IAulaRepository : IRepository<Aula>
    {
        Task<IList<Aula>> ObtenerTodasLasAulas();
    }
}
