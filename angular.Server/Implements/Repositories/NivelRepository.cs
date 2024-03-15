using Entities;
using Implements.Repositories.Base;
using Interfaces.Repositories;
using Context;

namespace Implements.Repositories
{
    public class NivelRepository: Repository<Nivel>, INivelRepository
    {
        public NivelRepository (ApplicationDbContext dBContext) : base(dBContext)
        {
        }
    }
}
