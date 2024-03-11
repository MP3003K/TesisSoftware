﻿using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Aula.Queries
{
    public class ListaAulasEstudianteQuery: IRequest<Response<IList<AulaDto>>>
    {
        public int EstudianteId { get; set; }
    }
    public class ListaAulaEstudianteHandler : IRequestHandler<ListaAulasEstudianteQuery, Response<IList<AulaDto>>>
    {
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudianteRepository;
        private readonly IMapper _mapper;

        public ListaAulaEstudianteHandler(
            IEvaluacionPsicologicaEstudianteRepository evaluacionPsicologicaEstudianteRepository,
            IMapper mapper)
        {
            _evaluacionPsicologicaEstudianteRepository = evaluacionPsicologicaEstudianteRepository;
            _mapper = mapper;
        }
        public async Task<Response<IList<AulaDto>>> Handle(ListaAulasEstudianteQuery request, CancellationToken cancellationToken)
        {            
            var aulas = await _evaluacionPsicologicaEstudianteRepository.ListaAulasDeEvaPsiEstPorIdEstudiante((int)request.EstudianteId);
            var aulasDto = _mapper.Map<IList<AulaDto>>(aulas);
            return new Response<IList<AulaDto>>(aulasDto);
        }
    }
}
