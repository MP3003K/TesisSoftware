    using Contracts.Repositories;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Repository.Context;
    using Repository.Repositories.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Repository.Repositories
    {
        public class EstudianteRepository: Repository<Estudiante>, IEstudianteRepository
        {
            public EstudianteRepository(ApplicationDbContext dBContext) : base(dBContext)
            {
            }

        }
    }
