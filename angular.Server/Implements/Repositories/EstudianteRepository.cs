using Domain.Entities;
using Repository.Context;
using Repository.Repositories.Base;
using webapi.Dao.Repositories;


namespace Repository.Repositories
{
    public class EstudianteRepository : Repository<Estudiante>, IEstudianteRepository
    {
        public EstudianteRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

    }
}
