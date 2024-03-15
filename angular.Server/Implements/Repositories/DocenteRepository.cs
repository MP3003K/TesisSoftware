using Domain.Entities;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
{
    public class DocenteRepository: Repository<Docente>, IDocenteRepository
    {
        public DocenteRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
