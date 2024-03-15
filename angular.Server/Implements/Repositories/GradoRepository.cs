using Entities;
using Implements.Repositories;
using Implements.Repositories.Base;
using Context;

namespace Implements.Repositories
{
    public class GradoRepository: Repository<Grado>, IGradoRepository
    {
        public GradoRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
