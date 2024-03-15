using Domain.Entities;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
{
    public class EscuelaRepository: Repository<Escuela>, IEscuelaRepository
    {
        public EscuelaRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
