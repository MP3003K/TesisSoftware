using Domain.Entities;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
{
    public class AccesoRepository : Repository<Acceso>, IAccesoRepository
    {
        public AccesoRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
