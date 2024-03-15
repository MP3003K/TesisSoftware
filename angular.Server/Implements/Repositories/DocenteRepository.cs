using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Context;

namespace Implements.Repositories
{
    public class DocenteRepository: Repository<Docente>, IDocenteRepository
    {
        public DocenteRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
