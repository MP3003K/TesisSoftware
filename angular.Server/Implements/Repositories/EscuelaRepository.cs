using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Context;

namespace Implements.Repositories
{
    public class EscuelaRepository: Repository<Escuela>, IEscuelaRepository
    {
        public EscuelaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
