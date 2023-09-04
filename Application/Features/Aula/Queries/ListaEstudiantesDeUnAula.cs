using Application.Exceptions;
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
    public class ListaEstudiantesDeUnAulaQuery : IRequest<Response<IList<EstudianteDto>>>
    {
        public int AulaId { get; set; }
        public int UnidadId { get; set; }
    }
    public class ListaEstudiantesDeUnAulaHandler : IRequestHandler<ListaEstudiantesDeUnAulaQuery, Response<IList<EstudianteDto>>>
    {
        private readonly IEvaluacionPsicologicaAulaRepository _evaluacionPsicologicaAulaRepository;
        private readonly IMapper _mapper;

        public ListaEstudiantesDeUnAulaHandler(
            IEvaluacionPsicologicaAulaRepository evaluacionPsicologicaAulaRepository,
            IMapper mapper)
        {
            _evaluacionPsicologicaAulaRepository = evaluacionPsicologicaAulaRepository;
            _mapper = mapper;
        }
        public async Task<Response<IList<EstudianteDto>>> Handle(ListaEstudiantesDeUnAulaQuery request, CancellationToken cancellationToken)
        {
            var evaluacionPsicologicaAula= await _evaluacionPsicologicaAulaRepository.EvaPsiAulaIncluyendoListaEstudiante(request.AulaId, request.UnidadId) ?? throw new EntidadNoEncontradaException(nameof(Domain.Entities.EvaluacionPsicologicaAula));

            if(evaluacionPsicologicaAula.EvaluacionesPsicologicasEstudiante == null || evaluacionPsicologicaAula.EvaluacionesPsicologicasEstudiante.Count <= 0)
                throw new EntidadNoEncontradaException(nameof(Domain.Entities.EvaluacionPsicologicaEstudiante));

            var listaEstudiantesDto = new List<EstudianteDto>();

            foreach (var evaluacion in evaluacionPsicologicaAula.EvaluacionesPsicologicasEstudiante)
            {
                if(evaluacion.Estudiante != null && evaluacion.Estudiante.Persona != null)
                    listaEstudiantesDto.Add(_mapper.Map<EstudianteDto>(evaluacion.Estudiante));
            }
            return new Response<IList<EstudianteDto>>(listaEstudiantesDto);
        }
    }
}