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

namespace Application.Features.Unidad.Queries
{
    public class ListaUnidadesEstudianteQuery : IRequest<Response<IList<UnidadDto>>>
    {
        public int EstudianteId;
    }
    public class ListaUnidadesEstudianteHandler : IRequestHandler<ListaUnidadesEstudianteQuery, Response<IList<UnidadDto>>>
    {
        private readonly IEvaluacionPsicologicaEstudianteRepository _evaluacionPsicologicaEstudiante;
        private readonly IMapper _mapper;

        public ListaUnidadesEstudianteHandler(
            IEvaluacionPsicologicaEstudianteRepository evaluacionPsicologicaEstudianteRepository,
            IMapper mapper)
        {
            _evaluacionPsicologicaEstudiante = evaluacionPsicologicaEstudianteRepository; 
            _mapper = mapper;
        }
        public async Task<Response<IList<UnidadDto>>> Handle(ListaUnidadesEstudianteQuery request, CancellationToken cancellationToken)
        {            
            var unidades = await _evaluacionPsicologicaEstudiante.ListaUnidadesDeEvaPsiEstPorIdEstudiante(request.EstudianteId) ?? throw new EntidadNoEncontradaException(nameof(Domain.Entities.Unidad));
   
            var unidadesDto = _mapper.Map<IList<UnidadDto>>(unidades);
            return new Response<IList<UnidadDto>>(unidadesDto);
        }
    }
}
