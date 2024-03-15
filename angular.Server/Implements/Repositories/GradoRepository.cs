using Domain.Entities;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;

namespace Repository.Repositories
{
    public class GradoRepository: Repository<Grado>, IGradoRepository
    {
        public GradoRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
